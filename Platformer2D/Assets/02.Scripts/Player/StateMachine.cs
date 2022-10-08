using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum StateType
    {
        Idle,
        Move,
        Jump,
        Fall,
        Attack,
        Crouch,
        EOF
    }
    public StateType Current;
    private Dictionary<StateType, StateBase> _states = new Dictionary<StateType, StateBase>();
    private StateBase _currentState;
    private CharacterBase _character;

    private float _h => Input.GetAxis("Horizontal");
    private float _v => Input.GetAxis("Vertical");
    private Vector2 _move;
    public bool IsMovable { get; set; }
    public bool IsDirectionChangable { get; set; }
    // -1 : left , 1 : right
    private int _direction;
    public int Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            if (value < 0)
            {
                _direction = -1;
                transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }
            else
            {
                _direction = 1;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }
    [SerializeField] private int _directionInit;




    private void Awake()
    {
        _character = GetComponent<CharacterBase>();
        Init();
    }

    private void Init()
    {
        for (StateType stateType = StateType.Idle; stateType < StateType.EOF; stateType++)
        {
            AddState(stateType);
        }
        _currentState = _states[StateType.Idle];
        Current = StateType.Idle;

        IsDirectionChangable = true;
        IsMovable = true;
    }

    private void AddState(StateType stateType)
    {
        // �̹� ���°� ��� �Ǿ��ٸ�
        if (_states.ContainsKey(stateType))
            return;

        string stateName = Convert.ToString(stateType);
        string typeName = "State" + stateName;
        Debug.Log($"[StateMachine] : Adding state ... {stateName}");

        Type type = Type.GetType(typeName);

        if (type != null)
        {
            ConstructorInfo constructor = type.GetConstructor(new Type[]
            {
                typeof(StateType),
                typeof(StateMachine)
            });

            StateBase state 
                = constructor.Invoke(new object[2]
                  {
                      stateType,
                      this
                  }) as StateBase;

            _states.Add(stateType, state);
            Debug.Log($"[StateMachine] : {stateName} state is successfully added");
        }
    }

    private void Update()
    {
        if (IsDirectionChangable)
        {
            if (_h < -0.1f)
                Direction = Constants.DIRECTION_LEFT;
            else if (_h > 0.1f)
                Direction = Constants.DIRECTION_RIGHT;
        }

        if (IsMovable)
        {
            _move.x = _h;

            if (Mathf.Abs(_move.x) > 0.1f)
                ChangeState(StateType.Move);
            else
                ChangeState(StateType.Idle);
        }

        ChangeState(_currentState.Update());

        if (Input.GetKey(KeyCode.LeftAlt))
            ChangeState(StateType.Jump);
    }
    private void FixedUpdate()
    {
        _currentState.FixedUpdate();
        transform.position += new Vector3(_move.x * _character.MoveSpeed, _move.y, 0.0f) * Time.fixedDeltaTime;
    }

    private void ChangeState(StateType newStateType)
    {
        // ���°� �ٲ��� �ʾ�����
        if (Current == newStateType)
            return;

        // �ٲٷ��� ���°� ���� �������� ������
        if (_states[newStateType].IsExecuteOK == false)
            return;

        _currentState.ForceStop(); // ���� ���� �ߴ�
        _currentState = _states[newStateType]; // ���� ����
        _currentState.Execute(); // ���ŵ� ���� ����
        Current = newStateType;
    }
}

using UnityEngine;

public class StateAttack : StateBase
{
    private LayerMask _enemyLayer;
    private Vector2 _boxcastCenter = new Vector2(0.16f, 0.16f);
    private Vector2 _boxcastSize = new Vector2(0.7f, 0.5f);


    public StateAttack(StateMachine.StateType machineType, StateMachine machine)
        : base(machineType, machine)
    {
        _enemyLayer = 1 << LayerMask.NameToLayer("Enemy");

    }

    public override bool IsExecuteOK => Machine.Current == StateMachine.StateType.Idle ||
                                        Machine.Current == StateMachine.StateType.Move ||
                                        Machine.Current == StateMachine.StateType.Jump ||
                                        Machine.Current == StateMachine.StateType.Fall;       

    public override void Execute()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void ForceStop()
    {
        Current = IState.Commands.Idle;
    }

    public override void MoveNext()
    {
    }

    public override StateMachine.StateType Update()
    {
        StateMachine.StateType next = MachineType;

        switch (Current)
        {
            case IState.Commands.Idle:
                break;
            case IState.Commands.Prepare:
                {
                    AnimationManager.Play("Attack");
                    MoveNext();
                }
                break;
            case IState.Commands.Casting:
                {
                    if (AnimationManager.IsCastingFinished)
                    {
                        Physics2D.BoxCast(Machine.transform + Vector3.right * Machine.Direction * _boxcastCenter, 0, _boxcastSize, 0, Vector2.zero, 0, _enemyLayer);

                        if(hit.collider)

                    }
                }
                break;
            case IState.Commands.OnAction:
                {
                   if(AnimationManager.GetNormalizedTime() >= 1.0f)
                    {
                        MoveNext();
                    }
                }
                break;
            case IState.Commands.Finish:
                {
                    next = StateMachine.StateType.Idle;
                }
                break;
            default:
                break;
        }

        return MachineType;
    }
}
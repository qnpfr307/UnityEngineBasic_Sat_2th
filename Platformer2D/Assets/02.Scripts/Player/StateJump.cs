using UnityEngine;
public class StateJump : StateBase
{
    private GroundDetector _groundDetector;
    private Rigidbody2D _rb;
    public StateJump(StateMachine.StateType machineType, StateMachine machine) 
        : base(machineType, machine)
    {
        _groundDetector = machine.GetComponent<GroundDetector>();
        _rb = machine.GetComponent<Rigidbody2D>();
    }

    public override bool IsExecuteOK => _groundDetector.IsDetected &&
                                        (Machine.Current == StateMachine.StateType.Idle ||
                                         Machine.Current == StateMachine.StateType.Move);

    public override void Execute()
    {
        Current = IState.Commands.Prepare;
        Machine.IsDirectionChangable = true;
        Machine.IsMovable = false;
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
        Current++;
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
                    Animator.Play("Jump");
                    _rb.velocity = new Vector2(_rb.velocity.x, 0.0f);
                    _rb.AddForce(Vector2.up * Character.JumpForce, ForceMode2D.Impulse);
                    MoveNext();
                }
                break;
            case IState.Commands.Casting:
                {
                    if (_groundDetector.IsDetected == false)
                        MoveNext();
                }
                break;
            case IState.Commands.OnAction:
                {
                    if (_rb.velocity.y < 0.0f ||
                        _groundDetector.IsDetected)
                        MoveNext();
                }
                break;
            case IState.Commands.Finish:
                {
                    if (_groundDetector.IsDetected)
                        next = StateMachine.StateType.Idle;
                    else
                        next = StateMachine.StateType.Fall;
                }
                break;
            default:
                break;
        }

        return next;
    }
}
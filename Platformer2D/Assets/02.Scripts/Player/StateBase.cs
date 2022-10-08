using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase : IState
{
    protected StateMachine.StateType MachineType;
    protected StateMachine Machine;
    protected Animator Animator;
    protected CharacterBase Character;
    public StateBase(StateMachine.StateType machineType, StateMachine machine)
    {
        MachineType = machineType;
        Machine = machine;
        Animator = Machine.GetComponent<Animator>();
        Character = Machine.GetComponent<CharacterBase>();
    }

    public IState.Commands Current { get; protected set; }

    public abstract bool IsExecuteOK { get; }

    public abstract void Execute();

    public abstract void FixedUpdate();

    public abstract void ForceStop();

    public virtual void MoveNext() => Current++;

    public abstract StateMachine.StateType Update();
}

using UnityEngine;

public abstract class State
{
    protected PlayerController _character;
    protected StateMachine _stateMachine;
    protected float _angle;

    protected State(PlayerController character, StateMachine stateMachine)
    {
        this._character = character;
        this._stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void HandleInput() 
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}

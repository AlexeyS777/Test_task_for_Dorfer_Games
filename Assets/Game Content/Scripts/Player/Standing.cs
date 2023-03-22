using UnityEngine;

public class Standing : State
{
    private MobileController _joystick;
    private string _objct;
    public Standing(PlayerController character, StateMachine stateMachine) : base(character, stateMachine)
    {
        _joystick = GameObject.FindObjectOfType<MobileController>();
    }
    
    public override void HandleInput()
    {
        base.HandleInput();
        _angle = _joystick.direction();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();  
        if (_angle != 0)
        {
            _stateMachine.ChangeState(_character._moveState);
        }
        else if (_objct == "Wheat")
        {
            _stateMachine.ChangeState(_character._harvestState);
        }        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _objct = _character.Scaning();
    }

    public override void Exit()
    {
        base.Exit();
        _objct = null;
    }
}

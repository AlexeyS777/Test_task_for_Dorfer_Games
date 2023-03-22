using UnityEngine;
//using UnityEngine.Events;

public class Move : State
{  
    private MobileController _joystick;

    public Move(PlayerController character, StateMachine stateMachine) : base(character, stateMachine)
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
        if (_angle == 0)
        {
            _stateMachine.ChangeState(_character._stayState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _character.Move(_character.Speed,_angle);
    }

    public override void Exit()
    {
        base.Exit();
        _character.Move(0, _character.transform.eulerAngles.y);
    }
}

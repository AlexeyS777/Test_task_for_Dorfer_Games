using UnityEngine;

public class Harvest : State
{  
    public Harvest(PlayerController character, StateMachine stateMachine) : base(character, stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        _character.Harvest();
    }
}

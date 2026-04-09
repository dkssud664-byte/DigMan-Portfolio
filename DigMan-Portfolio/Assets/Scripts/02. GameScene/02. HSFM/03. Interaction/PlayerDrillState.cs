using UnityEngine;

public class PlayerDrillState : PlayerInteractionState
{
    public PlayerDrillState(PlayerController player,
        PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        
    }
    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
    }
}

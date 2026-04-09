using UnityEngine;

public class PlayerRemoteState : PlayerInteractionState
{
    public PlayerRemoteState(PlayerController player,
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

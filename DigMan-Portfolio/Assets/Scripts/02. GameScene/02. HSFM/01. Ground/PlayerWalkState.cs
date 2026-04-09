using UnityEngine;

public class PlayerWalkState : PlayerGroundState
{
    public  PlayerWalkState(PlayerController player,
        PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("°È±â");
    }

    public override void Update()
    {
        base.Update();

        if (player.Input.LeftShiftHold && player.CharacterController.isGrounded)
        {
            stateMachine.ChangeState(player.PlayerRunState);
            return;
        }
    }

    public override void Exit()
    {

    }
}

using UnityEngine;

public class PlayerRunState : PlayerGroundState
{
    public  PlayerRunState(PlayerController player,
        PlayerStateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        Debug.Log("¶Ù±â");
    }
    public override void Update()
    {
        base.Update();

        if (!player.Input.LeftShiftHold && player.CharacterController.isGrounded)
        {
            stateMachine.ChangeState(player.PlayerWalkState);
            return;
        }
    }
    public override void Exit()
    {

    }
}

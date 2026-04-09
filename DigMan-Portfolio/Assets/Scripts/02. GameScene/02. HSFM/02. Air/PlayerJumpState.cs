using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    private bool jumped;

    public PlayerJumpState(PlayerController player,
        PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("มกวม");
        jumped = false;
        player.Jump();
    }

    public override void Update()
    {
        player.ApplyGravity();
        player.Move(player.Input.Move);

        if (!jumped)
        {
            if (!player.CharacterController.isGrounded)
            {
                jumped = true;
            }

            return;
        }

        base.Update();
    }

    public override void Exit()
    {
    }
}

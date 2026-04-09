using UnityEngine;
public class PlayerGroundState : PlayerState
{
    
    public PlayerGroundState(PlayerController player, PlayerStateMachine sm)
        : base(player, sm)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        if (player.Input.SpaceDown && player.CharacterController.isGrounded)
        {
            stateMachine.ChangeState(player.PlayerJumpState);
            return;
        }

        base.Update();

        if (move != Vector2.zero && player.CharacterController.isGrounded)
        {
            stateMachine.ChangeState(player.PlayerWalkState);
            return;
        }

        if (move == Vector2.zero)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
            return;
        }
    }

    public override void Exit()
    {

    }
}
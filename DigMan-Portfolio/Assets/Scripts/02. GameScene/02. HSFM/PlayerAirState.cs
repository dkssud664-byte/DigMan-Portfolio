using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(PlayerController player,
        PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        base.Update();

        if (move == Vector2.zero && player.CharacterController.isGrounded)
        {
            stateMachine.ChangeState(player.PlayerIdleState);
            return;
        }

        if(player.CharacterController.isGrounded)
        {
            stateMachine.ChangeState(player.PlayerWalkState);
            return;
        }
    }

    public override void Exit()
    {

    }
}

using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerController player,
        PlayerStateMachine sm) : base(player, sm)
    {
    }

    public override void Enter()
    {
        Debug.Log("Idle ป๓ลย");
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {

    }


    
}

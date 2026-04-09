using UnityEngine;

public class PlayerJetpackState : PlayerAirState
{
    public PlayerJetpackState(PlayerController player,
        PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Á¦Æ®ÆÑ »óÅÂ");
        player.StartJetpack();
    }
    public override void Update()
    {
        base.Update();
        player.JetpackMove();
    }
    public override void Exit()
    {
        player.StopJetpack();
    }
}

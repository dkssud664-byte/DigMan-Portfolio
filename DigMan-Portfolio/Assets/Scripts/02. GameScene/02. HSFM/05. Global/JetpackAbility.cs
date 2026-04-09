using UnityEngine;

public class JetpackAbility
{
    private PlayerController player;
    private PlayerStatsSystem stats;
    private float delay;

    private float staminaCost = 1.5f;

    public JetpackAbility(PlayerController player, PlayerStatsSystem stats)
    {
        this.player = player;
        this.stats = stats;
        delay = 1f;
    }

    public void Update()
    {
        if(player.Input.SpaceDown)
        {
            player.Input.ClearSpaceDownInput();
            delay = 1f;
        }

        if (player.Input.SpaceHold)
        {
            delay -= Time.deltaTime;
            if (delay <= 0f)
            {
                if (!player.Input.SpaceHold)
                {
                    return;
                }

                if (!stats.UseStamina(staminaCost))
                {
                    return;
                }
                Debug.Log("jetpackMove ÁøÀÔ");
                player.JetpackMove();
            }
            
        }
    }
}

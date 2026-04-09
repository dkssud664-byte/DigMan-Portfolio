using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "PlayerStat/PlayerStatDatabase")]
public class PlayerStatLevelDatabase : ScriptableObject
{
    public List<PlayerStatLevelData> stats;

    public PlayerStatLevelData Get(PlayerStatType type)
    {
        return stats.Find(e => e.Type == type);
    }
}

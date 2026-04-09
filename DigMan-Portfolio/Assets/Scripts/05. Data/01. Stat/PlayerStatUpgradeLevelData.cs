using System;

[Serializable]
public class PlayerStatUpgradeLevelData
{
    public PlayerStatType type;
    public int level;

    public PlayerStatUpgradeLevelData(PlayerStatType type)
    {
        this.type = type;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerUpgradeLevel
{
    //스텟레벨
    [SerializeField]
    private List<PlayerStatUpgradeLevelData> statLevels = new();
    //장비레벨
    [SerializeField]
    private List<PlayerEquipUpgradeLevelData> equipsLevels = new();

    private Dictionary<PlayerStatType, PlayerStatUpgradeLevelData> statMap;
    private Dictionary<EquipType, PlayerEquipUpgradeLevelData> equipMap;

    public void Initialize()
    {
        statLevels.Clear();
        equipsLevels.Clear();

        foreach (PlayerStatType type in Enum.GetValues(typeof(PlayerStatType)))
        {
            statLevels.Add(new PlayerStatUpgradeLevelData(type));
        }

        foreach (EquipType type in Enum.GetValues(typeof(EquipType)))
        {
            PlayerEquipUpgradeLevelData data = new PlayerEquipUpgradeLevelData();
            data.Initialize(type);
            equipsLevels.Add(data);
        }
        BuildRuntimeCache();
    }

    public void BuildRuntimeCache()
    {
        statMap = statLevels.ToDictionary(x => x.type);
        equipMap = equipsLevels.ToDictionary(x => x.type);
    }

    public PlayerStatUpgradeLevelData GetStat(PlayerStatType type)
    {
        return statMap[type];
    }

    public PlayerEquipUpgradeLevelData GetEquip(EquipType type)
    {
        return equipMap[type];
    }
}

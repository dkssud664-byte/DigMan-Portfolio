using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerEquipUpgradeLevelData
{
    public EquipType type;
    [SerializeField] List<EquipStatLevel> stats = new();
    private Dictionary<EquipStatType, EquipStatLevel> statMap = new();

    public void Initialize(EquipType type)
    {
        this.type = type;

        stats.Clear();
        statMap.Clear();

        foreach (EquipStatType statType in Enum.GetValues(typeof(EquipStatType)))
        {
            EquipStatLevel level = new EquipStatLevel();
            level.type = statType;
            stats.Add(level);
        }

        if (statMap.Count == 0)
        {
            foreach (EquipStatLevel stat in stats)
            {
                statMap[stat.type] = stat;
            }
        }
    }

    public EquipStatLevel GetStat(EquipStatType type)
    {
        if(statMap.Count == 0)
        {
            foreach (EquipStatLevel stat in stats)
            {
                statMap[stat.type] = stat;
            }
        }
        return statMap[type];
    }
}

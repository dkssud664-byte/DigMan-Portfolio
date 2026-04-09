using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Equip/EquipmentData")]
public class EquipmentData : ScriptableObject
{
    [SerializeField] protected EquipType type;
    [SerializeField] protected string equipName;
    [SerializeField] protected Sprite icon;
    [SerializeField] List<EquipStatLevelData> levelData;
    [SerializeField] protected int unlockCost;
    [SerializeField] protected float coolTime;
    [SerializeField] protected int maxQuantity;
    [SerializeField] protected int quantity;
    [SerializeField] protected int range;

    public EquipType Type => type;
    public string EquipName => equipName;
    public Sprite Icon => icon;
    public int UnlockCost => unlockCost;
    public float CoolTime => coolTime;
    public int MaxQuantity => maxQuantity;
    public int Quantity => quantity;
    public int Range => range;

    public EquipStatLevelData GetStat(EquipStatType type)
    {
        return levelData.Find(x => x.type == type);
    }
}
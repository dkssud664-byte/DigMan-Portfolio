using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Equip/EquipmentDatabase")]
public class EquipmentDatabase : ScriptableObject
{
    public List<EquipmentData> equipments;

    public EquipmentData Get(EquipType type)
    {
        return equipments.Find(e => e.Type == type);
    }
}
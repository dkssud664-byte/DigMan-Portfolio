using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Mineral/MineralDatabase")]
public class MineralDatabase : ScriptableObject
{
    [SerializeField] private List<MineralData> mineralDatas;

    public MineralData Get(MineralType type)
    {
        return mineralDatas.Find(e => e.Type == type);
    }
}

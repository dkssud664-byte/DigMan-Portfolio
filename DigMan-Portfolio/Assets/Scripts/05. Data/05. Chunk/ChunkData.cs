using UnityEngine;
using System.Collections.Generic;

public class ChunkData
{
    public Vector2Int index;
    public List<MineralSpawnData> minerals = new List<MineralSpawnData>();
    public GameObject parent;
}

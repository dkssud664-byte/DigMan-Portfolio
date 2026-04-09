using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class MineralGenerator : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private MineralDepthConfig config;
    [SerializeField] private MineralDatabase database;
    [SerializeField] private Transform chunkRoot;
    [SerializeField] private GameObject[] mineralPrefabs;

    public Dictionary<Vector2Int, ChunkData> Generate()
    {
        Dictionary<Vector2Int, ChunkData> chunks
            = new Dictionary<Vector2Int, ChunkData>();

        float bottom = terrain.transform.position.y;
        float top = bottom + terrain.terrainData.size.y;

        float spacing = 20f;
        float chunkSize = 100f;

        int number = 0;
        for (float y = bottom; y <= top; y += spacing)
        {
            for (float x = 0; x < terrain.terrainData.size.x; x += spacing)
            {
                for (float z = 0; z < terrain.terrainData.size.z; z += spacing)
                {
                    Vector3 pos = new Vector3(
                        terrain.transform.position.x + x,
                        y,
                        terrain.transform.position.z + z
                    );

                    var rule = config.DepthRules
                        .FirstOrDefault(r => pos.y >= r.MinY && pos.y < r.MaxY);

                    if (rule == null) continue;

                    float rand = Random.value;
                    float cumulative = 0f;

                    foreach (var mineral in rule.Minerals)
                    {
                        cumulative += mineral.Ratio;
                        if (rand <= cumulative)
                        {
                            int chunkX = Mathf.FloorToInt(
                            (pos.x - terrain.transform.position.x) / chunkSize);
                            int chunkZ = Mathf.FloorToInt(
                                (pos.z - terrain.transform.position.z) / chunkSize);

                            Vector2Int index = new Vector2Int(chunkX, chunkZ);

                            if (!chunks.TryGetValue(index, out ChunkData chunk))
                            {
                                chunk = new ChunkData();
                                chunk.index = index;
                                chunks.Add(index, chunk);
                            }

                            chunk.minerals.Add(new MineralSpawnData
                            {
                                type = mineral.Type,
                                number = number++,
                                position = pos
                            });
                            break;
                        }
                    }
                }
            }
        }

        return chunks;
    }

    public void CreateMinerals(ChunkData data)
    {
        int rength = data.minerals.Count;
        int prefabCount = mineralPrefabs.Length;

        if(data.parent == null)
        {
            data.parent = new GameObject(data.index.ToString());
            data.parent.transform.position = Vector3.zero;
            data.parent.transform.SetParent(chunkRoot, true);
        }

        for (int i = 0; i < rength; i++)
        {
            if (data.minerals[i].prefabIndex <= 0)
            {
                int prefabIndex = Random.Range(0, prefabCount);
                data.minerals[i].prefabIndex = prefabIndex;
            }

            if(mineralPrefabs[data.minerals[i].prefabIndex] != null)
            {
                GameObject mineral = Instantiate(mineralPrefabs[data.minerals[i].prefabIndex],
                    data.minerals[i].position, Quaternion.identity, data.parent.transform
                    );
            }
        }
    }
}


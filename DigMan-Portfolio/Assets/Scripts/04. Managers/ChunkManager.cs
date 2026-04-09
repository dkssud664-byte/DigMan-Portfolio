using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [SerializeField] private Terrain terrain;
    [SerializeField] private float chunkSize = 100f;
    [SerializeField] private int viewDistance = 1;
    private Transform player;

    private Vector3 terrainOrigin;
    private Vector3 terrainSize;

    public int ChunkCountX { get; private set; }
    public int ChunkCountZ { get; private set; }
    private Vector2Int currentChunkIndex;
    private Vector2Int chunkIndex;

    private int showRange;

    private Dictionary<Vector2Int, ChunkData> chunks;

    private void Awake()
    {
        
    }

    private void Start()
    {
        player = Facade.Instance.PlayerManager.Player.transform;
        chunkIndex = GetChunkIndex(player.position);
        ShowChunkIndex(player.position, chunks);
    }

    private void FixedUpdate()
    {
        currentChunkIndex = GetChunkIndex(player.position);

        if (currentChunkIndex != chunkIndex)
        {
            chunkIndex = currentChunkIndex;
            Debug.Log("청크 이동");
            ShowChunkIndex(player.position, chunks);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("currentChunkIndex" + currentChunkIndex);
            Debug.Log("chunkIndex" + chunkIndex);
            Debug.Log("player" + player.position);
        }
    }


    public void Init()
    {
        CacheTerrainData();
    }

    public void SetChunkData(Dictionary<Vector2Int, ChunkData> chunkData)
    {
        if(chunkData == null)
        {
            return;
        }

        chunks = chunkData;
    }

    private void CacheTerrainData()
    {
        if (terrain == null)
        {
            return;
        }

        terrainOrigin = terrain.transform.position;
        terrainSize = terrain.terrainData.size;

        ChunkCountX = Mathf.CeilToInt(terrainSize.x / chunkSize);
        ChunkCountZ = Mathf.CeilToInt(terrainSize.z / chunkSize);

        Debug.Log($"Chunk Count: {ChunkCountX} x {ChunkCountZ}");
    }

    public Vector2Int GetChunkIndex(Vector3 worldPos)
    {
        float localX = worldPos.x - terrainOrigin.x;
        float localZ = worldPos.z - terrainOrigin.z;

        if (localX < 0 || localZ < 0 ||
            localX >= terrainSize.x || localZ >= terrainSize.z)
        {
            return new Vector2Int(-1, -1);
        }

        int x = Mathf.FloorToInt(localX / chunkSize);
        int z = Mathf.FloorToInt(localZ / chunkSize);

        return new Vector2Int(x, z);
    }

    public void ShowChunkIndex(Vector3 worldPos, Dictionary<Vector2Int, ChunkData> chunks)
    {
        Vector2Int index = GetChunkIndex(worldPos);
        List<Vector2Int> activeIndex = new List<Vector2Int>();

        foreach(KeyValuePair<Vector2Int, ChunkData> kvp in chunks)
        {
            kvp.Value.parent.SetActive(false);
        }

        //활성화 인덱스 저장
        for(int i = -viewDistance; i <= viewDistance; i++)
        {
            for(int j = -viewDistance; j <= viewDistance; j++)
            {
                Vector2Int tempIndex = new Vector2Int(index.x + i, index.y + j);

                if(i < 0 || j < 0)
                {
                    continue;
                }

                activeIndex.Add(tempIndex);

                if(chunks.ContainsKey(tempIndex))
                {
                    chunks[tempIndex].parent.SetActive(true);
                }
            }
        }
        Debug.Log("activeIndex " + activeIndex.Count);
    }
}

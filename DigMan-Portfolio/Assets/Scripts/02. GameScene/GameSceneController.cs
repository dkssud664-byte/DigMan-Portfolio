using System;
using UnityEngine;
using System.Collections.Generic;

public class GameSceneController : MonoBehaviour, ISaveable
{
    [NonSerialized] public GameSaveData currentSaveData;

    //터레인
    [SerializeField] private Terrain terrain;
    public Terrain Terrain => terrain;

    //플레이어 관련
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerCanvasPrefab;
    private PlayerSpawner playerSpawner;
    public GameObject Player { get; private set; }
    public GameObject PlayerCanvas { get; private set; }

    //세이브 파일유무
    private bool isSaveData = true;

    //퍼즈메뉴
    [SerializeField] private PauseMenuCanvas pauseMenuCanvas;

    //광물생성
    [SerializeField] private MineralGenerator mineralGenerator;
    private Dictionary<Vector2Int, ChunkData> chunkDatas;

    //청크매니저
    [SerializeField] private ChunkManager chunkManager;


    private void Awake()
    {
        Facade.Instance.UIManager.SetPauseMenuCanvas(pauseMenuCanvas);
        Facade.Instance.SetGameSceneController(this);

        //세이브 파일유무
        if (SaveLoadManager.Instance.LoadSaveData(
            SaveLoadManager.Instance.LoadIndex) == null)
        {
            isSaveData = false;
        }
        //로드
        if (isSaveData)
        {
            currentSaveData = SaveLoadManager.Instance.LoadSaveData(
            SaveLoadManager.Instance.LoadIndex);

            //맵 초기화
            currentSaveData.mapData.LoadTerrain(terrain);
        }
        else
        {
            //불러오기
            currentSaveData = new GameSaveData().Init();
            currentSaveData.mapData.Init(terrain);

            //광물 생성
            chunkManager.Init();
            chunkDatas = mineralGenerator.Generate();
            chunkManager.SetChunkData(chunkDatas);

            //임시
            for(int i = 0; i < chunkManager.ChunkCountX; i++)
            {
                for(int j = 0; j < chunkManager.ChunkCountZ; j++)
                {
                    mineralGenerator.CreateMinerals(chunkDatas[new Vector2Int(i, j)]);
                }
            }
        }

        //플레이어, 플레이어 UI 생성 후 초기화
        playerSpawner = new PlayerSpawner();
        Player = playerSpawner.CreatePlayer(playerPrefab);
        
        PlayerController playerController = Player.GetComponent<PlayerController>();
        PlayerCanvas = playerSpawner.CreatePlayerCanvas(playerCanvasPrefab);
        playerController.SetPlayerCanvas(PlayerCanvas.GetComponent<PlayerCanvas>());
        Facade.Instance.PlayerManager.SetPlayerCanvas(PlayerCanvas.GetComponent<PlayerCanvas>());
        playerSpawner.InitializePlayer(Player, currentSaveData);
        Facade.Instance.PlayerManager.PlayerInteraction.
            SetInteractionUI(PlayerCanvas.GetComponent<PlayerCanvas>().InteractionArea);
    }

    private void Start()
    {
        Facade.Instance.MapCamera.SetPlayer(Player.transform);
    }

    public void Load(GameSaveData data)
    {
        currentSaveData = data;
    }

    public void Save()
    {
        PlayerInfo playerinfo = Player.GetComponent<PlayerInfo>();
        currentSaveData.playerData = playerinfo.PlayerInfoConvertToPlayerData();
        currentSaveData.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

        currentSaveData.mapData.SaveFromTerrain(terrain);
    }
}

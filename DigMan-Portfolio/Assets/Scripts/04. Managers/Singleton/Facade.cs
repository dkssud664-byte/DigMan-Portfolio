using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Facade : MonoBehaviour
{
    public static Facade Instance { get; private set; }

    #region 싱글톤 매니저들
    public SoundManager SoundManager => SoundManager.Instance;       
    public SceneManagerWrapper SceneManager => SceneManagerWrapper.Instance;  
    public SettingManager SettingManager => SettingManager.Instance;
    public LoadingManager LoadingManager => LoadingManager.Instance;
    public SaveLoadManager SaveManager => SaveLoadManager.Instance;
    #endregion

    //UI 관리
    [SerializeField] private UIManager uIManager;
    public UIManager UIManager => uIManager;

    //플레이어
    public PlayerManager PlayerManager { get; private set; }

    [SerializeField] private InputSystem inputSystem;
    public InputSystem InputSystem => inputSystem;

    //게임씬 관리
    public GameSceneController GameSceneController { get; private set; }

    //맵 카메라
    public MapCamera MapCamera { get; private set; }

    //게임 매니저
    public GameManager GameManager { get; private set; }

    private void Awake()
    {
        //싱글톤 중복 방지
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetGameManager(GameManager gameManager)
    {
        if(gameManager ==null)
        {
            return;
        }

        this.GameManager = gameManager;
    }
  

    public void SetGameSceneController(GameSceneController gameSceneController)
    {
        if(gameSceneController == null)
        {
            return;
        }

        this.GameSceneController = gameSceneController;
    }

    public void SetPlayerManager(PlayerManager playerManager)
    {
        if (playerManager == null)
        {
            return;
        }

        this.PlayerManager = playerManager;
    }

    public void SetMapCamera(MapCamera mapCamera)
    {
        if(mapCamera == null)
        {
            return;
        }

        this.MapCamera = mapCamera;
    }


}

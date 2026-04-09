using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Menu")]
    [Header("Buttons")]
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button exitBtn;

    [Header("Area")]
    [SerializeField] private UIPanel loadArea;
    [SerializeField] private UIPanel settingArea;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = Facade.Instance.UIManager;
    }

    private void Start()
    {
        NewGameButton();
        LoadButton();
        SettingButton();
        ExitButton();
    }

    //게임씬 이동
    private void NewGameButton()
    {
        newGameBtn.onClick.AddListener(() =>
        {
            LoadingManager.Instance.AsyncLoadScene(Scenes.Game);
        });

    }

    private void LoadButton()
    {
        loadBtn.onClick.AddListener(() => {
            uiManager.CloseAll();
            uiManager.Open(loadArea);
        });
    }


    private void SettingButton()
    {
        //메뉴의 세팅 버튼을 누르면 세팅창 활성화
        settingBtn.onClick.AddListener(() =>
        {
            uiManager.CloseAll();
            uiManager.Open(settingArea);
        });
    }

    private void ExitButton()
    {
        //종료버튼
        exitBtn.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        });
    }

    private void OtherAreaDisable()
    {

    }
}

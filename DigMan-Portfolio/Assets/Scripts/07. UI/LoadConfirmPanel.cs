using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadConfirmPanel : UIPanel
{
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;

    public GameObject confirmPanel;
    public TextMeshProUGUI lodingText;

    private void Awake()
    {
        //버튼 초기화
        InitializeCancelButton();
        InitializeConfirmButton();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void InitializeConfirmButton()
    {
        confirmButton.onClick.AddListener(() =>
        {
            LoadingManager.Instance.AsyncLoadScene(Scenes.Game);
        });
    }

    private void InitializeCancelButton()
    {
        cancelButton.onClick.AddListener(() =>
        {
            Facade.Instance.UIManager.CloseTop();
        });
    }
}

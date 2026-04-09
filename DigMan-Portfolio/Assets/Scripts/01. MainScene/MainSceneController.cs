using TMPro;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] private GameObject loadArea;           //로드UI 영역
    [SerializeField] private GameObject settingArea;        //세팅UI 영역
    [SerializeField] private GameObject loadingPanel;       //로딩UI 패널
    [SerializeField] private TextMeshProUGUI loadingText;   //로딩UI 텍스트


    private void Awake()
    {
        LoadingManager.Instance.SetLoadingUI(loadingPanel, loadingText);    
    }

    void Start()
    {
        
    }

    void Update()
    {
        //ESC 누름
        if(Facade.Instance.InputSystem.ESCDown)
        {
            ShutDownArea();
        }
    }

    void ShutDownArea()
    {
        if(loadArea.activeSelf)
        {
            loadArea.SetActive(false);
        }

        if(settingArea.activeSelf)
        {
            settingArea.SetActive(false);
        }
    }


}

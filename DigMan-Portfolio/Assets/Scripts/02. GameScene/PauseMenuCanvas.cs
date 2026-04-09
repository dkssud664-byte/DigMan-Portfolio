using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuCanvas : UIPanel
{
    [Header("Pause Menu Buttons")]
    [SerializeField] Button continueBtn;
    [SerializeField] Button quickSaveBtn;
    [SerializeField] Button saveBtn;
    [SerializeField] Button loadBtn;
    [SerializeField] Button settingBtn;
    [SerializeField] Button exitBtn;
    [SerializeField] Button closeBtn;

    [Header("Areas")]
    [SerializeField] UIPanel saveArea;
    [SerializeField] UIPanel loadArea;
    private LoadUI loadUI;
    [SerializeField] UIPanel settingArea;

    private UIManager uiManager;

    private void Awake()
    {
        uiManager = Facade.Instance.UIManager;

        //버튼 초기화
        InitializeContinuBtn();
        InitializeQuickSaveBtn();
        InitializeSaveBtn();
        InitializeLoadBtn();
        InitializeSettingBtn();
        InitializeExitBtn();
        InitializeCloseBtn();

        loadUI = loadArea.gameObject.GetComponent<LoadUI>();

        //로딩 게이지 텍스트 할당
        LoadingManager.Instance.SetLoadingUI(
            loadUI.ConfirmPanel.confirmPanel,
            loadUI.ConfirmPanel.lodingText
            );

    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    private void InitializeContinuBtn()
    {
        if (continueBtn == null)
        {
            return;
        }

        continueBtn.onClick.AddListener(() => {
            uiManager.CloseTop();
        });
    }

    private void InitializeQuickSaveBtn()
    {
        if(quickSaveBtn == null)
        {
            return;
        }

        quickSaveBtn.onClick.AddListener(() => { });
    }

    private void InitializeSaveBtn()
    {
        if (saveBtn == null)
        {
            return;
        }

        saveBtn.onClick.AddListener(() => {
            uiManager.Open(saveArea);
        });
    }

    private void InitializeLoadBtn()
    {
        if (loadBtn == null)
        {
            return;
        }
        loadBtn.onClick.AddListener(() => {
            uiManager.Open(loadArea);
        });
    }

    private void InitializeSettingBtn()
    {
        if(settingBtn == null)
        {
            return;
        }
        settingBtn.onClick.AddListener(() => {
            uiManager.Open(settingArea);
        });
    }

    private void InitializeExitBtn()
    {
        if(exitBtn == null)
        {
            return;
        }
        exitBtn.onClick.AddListener(() => {
            LoadingManager.Instance.LoadScene(Scenes.Main);
        });
    }

    private void InitializeCloseBtn()
    {
        if (closeBtn == null)
        {
            return;
        }
        closeBtn.onClick.AddListener(() => {
            uiManager.CloseTop();
        });
    }
}

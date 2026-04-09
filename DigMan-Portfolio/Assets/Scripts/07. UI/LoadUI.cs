using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : UIPanel
{
    [SerializeField] private Button closeBtn;       //닫기 버튼
    [SerializeField] private Button[] loadBtn;      //로드 버튼
    private SaveDataArea[] saveAreas;               //버튼오브젝트 안의 스크립트
    [SerializeField] LoadConfirmPanel confirmPanel; //씬이동 재확인
    public LoadConfirmPanel ConfirmPanel => confirmPanel;   //안의 자식 넘기기
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = Facade.Instance.UIManager;

        //로드 UI 가져오기
        saveAreas = new SaveDataArea[loadBtn.Length];
        SetSaveDataArea();

        InitializeCloseButton();
        InitializeLoadButton();
    }

    private void OnEnable()
    {
        InitializeLoadArea();
    }

    void Start()
    {
        for (int i = 0; i < loadBtn.Length; i++)
        {
            GameSaveData data = SaveLoadManager.Instance.GameSaveDatas[i];
            saveAreas[i].SetData(data.saveTime, data.playerData.money.ToString(),
                data.playerData.maxDepth.ToString());
        }
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    private void SetSaveDataArea()
    {
        for(int i = 0; i < loadBtn.Length; i++)
        {
            saveAreas[i] = loadBtn[i].gameObject.GetComponent<SaveDataArea>();        
        }
    }

    private void InitializeCloseButton()
    {
        closeBtn.onClick.AddListener(() => {
            uiManager.CloseTop();
        });
    }

    private void InitializeLoadButton()
    {
        for (int i = 0; i < loadBtn.Length; i++)
        {
            int index = i;
            loadBtn[i].onClick.AddListener(() =>
            {
                SaveLoadManager.Instance.SetLoadIndex(index);
                Facade.Instance.UIManager.Open(confirmPanel);
            });
        }
    }

    private void InitializeLoadArea()
    {
        for (int i = 0; i < saveAreas.Length; i++)
        {
            GameSaveData saveData = SaveLoadManager.Instance.GameSaveDatas[i];
            saveAreas[i].SetData(
                saveData.saveTime,
                saveData.playerData.money.ToString(),
                saveData.playerData.maxDepth.ToString()
                );
        }
    }
}

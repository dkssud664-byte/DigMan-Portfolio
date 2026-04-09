using UnityEngine;
using UnityEngine.UI;

public class SaveUI : UIPanel
{
    [SerializeField] Button closeBtn;       //닫기버튼
    [SerializeField] Button[] saveBtn;      //세이브 버튼들
    private SaveDataArea[] saveDataAreas;   //세이브 버튼 객체의 스크립트
    private PlayerManager playerManager;    //현재 게임 정보를 가져오기 위한 변수

    private void Awake()
    {
        closeBtn.onClick.AddListener(() => {
            Facade.Instance.UIManager.CloseTop();
        });
        saveDataAreas = new SaveDataArea[saveBtn.Length];

        for(int i = 0; i < saveDataAreas.Length; i++)
        {
            saveDataAreas[i] = saveBtn[i].gameObject.GetComponent<SaveDataArea>();
        }

        playerManager = Facade.Instance.PlayerManager;
    }

    private void Start()
    {
        InitializeSaveButton();
    }

    private void OnEnable()
    {
        InitializeSaveArea();
    }

    private void InitializeSaveArea()
    {
        for(int i = 0; i < saveDataAreas.Length; i++)
        {
            GameSaveData saveData = SaveLoadManager.Instance.GameSaveDatas[i];
            saveDataAreas[i].SetData(
                saveData.saveTime,
                saveData.playerData.money.ToString(),
                saveData.playerData.maxDepth.ToString()
                );
        }
    }

    private void InitializeSaveButton()
    {
        for(int i = 0; i < saveBtn.Length; i++)
        {
            int index = i;

            saveBtn[i].onClick.AddListener(() => {
                GameSaveData gameSaveData = Facade.Instance.GameSceneController.currentSaveData;

                SaveLoadManager.Instance.SaveGameData(index);
                saveDataAreas[index].SetData(
                    gameSaveData.saveTime,
                    gameSaveData.playerData.money.ToString(),
                    gameSaveData.playerData.maxDepth.ToString());
            });
        }
    }
}

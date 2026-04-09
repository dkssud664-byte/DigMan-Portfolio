using UnityEngine;
using System.IO;
using System;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    private string saveSettingPath;
    private string[] gameSavePath = new string[5];
    private GameSaveData[] gameSaveDatas = new GameSaveData[5];
    public GameSaveData[] GameSaveDatas { get { return gameSaveDatas; } }

    //로드 인덱스
    public int LoadIndex { get; private set; }

    private void Awake()
    {
        //싱글톤
        if(Instance == null)
        {
            Instance = this;
            saveSettingPath = Application.persistentDataPath + "/Satting_Save.json";

            for (int i = 0; i < gameSavePath.Length; i++)
            {
                gameSavePath[i] = Application.persistentDataPath + $"/Game_Save_{i}";
            }
        }
        else
        {
            Destroy(this.gameObject);
        }

        for(int i = 0; i < GameSaveDatas.Length; i++)
        {
            GameSaveDatas[i] = LoadSaveData(i) ?? new GameSaveData().Init();
        }

        //기본값
        LoadIndex = 0;
    }

    //세팅 저장
    public void SaveSetting()
    {
        SettingData settingData = SettingManager.Instance.currentSetting;
        string json = JsonUtility.ToJson(settingData, true);
        File.WriteAllText(saveSettingPath, json);
    }

    //세팅 불러오기
    public SettingData LoadSetting()
    {
        if(!File.Exists(saveSettingPath))
        {
            return null;
        }

        string json = File.ReadAllText(saveSettingPath);
        return JsonUtility.FromJson<SettingData>(json);
    }

    //게임 저장
    public void SaveGameData(int index)
    {
        //인게임 데이터 넣기
        GameSceneController gameSceneController = Facade.Instance.GameSceneController;
        gameSceneController.Save();
        gameSaveDatas[index] = Facade.Instance.GameSceneController.currentSaveData;

        //제이슨으로 저장
        string json = JsonUtility.ToJson(gameSaveDatas[index], true);
        File.WriteAllText(gameSavePath[index], json);

    }

    //게임 불러오기
    public GameSaveData LoadSaveData(int index)
    {
        if (!File.Exists(gameSavePath[index]))
        {
            return null;
        }

        string json = File.ReadAllText(gameSavePath[index]);
        return JsonUtility.FromJson<GameSaveData>(json);
    }

    public void SetLoadIndex(int index)
    {
        this.LoadIndex = index;
    }

}

using System;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }

    [NonSerialized] public SettingData currentSetting;  //현재 세팅 정보

    private void Awake()
    {
        //싱글톤
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //현재 모니터 정보
        Resolution r = Screen.currentResolution;
        currentSetting = SaveLoadManager.Instance.LoadSetting() ?? new SettingData().Init();

        //첫 화면 초기화
        Screen.SetResolution(currentSetting.windowSizeWidth, currentSetting.windowSizeHeight, currentSetting.isFullScreen);
    }


    //마스터 불륨 수정
    public void SetMaster(float value)
    {
        currentSetting.master = value;
        SaveLoadManager.Instance.SaveSetting();
        SoundManager.Instance.SetMaster(value);
    }

    //BGM 볼륨 수정
    public void SetBGM(float value)
    {
        currentSetting.bgm = value;
        SaveLoadManager.Instance.SaveSetting();
        SoundManager.Instance.SetBGM(value);
    }

    //SFX 볼륨 수정
    public void SetSFX(float value)
    {
        currentSetting.sfx = value;
        SaveLoadManager.Instance.SaveSetting();
        SoundManager.Instance.SetSFX(value);
    }

    //감도 수정
    public void SetSensitivity(int value)
    {
        currentSetting.sensitivity = value;
    }

    //윈도우 사이즈
    public void SetWindowSize(int width, int height, int index = 0)
    {
        currentSetting.windowSizeWidth = width;
        currentSetting.windowSizeHeight = height;
        currentSetting.windowDropdownIndex = index;
    }
    
    public void SetFullScreen(bool value)
    {
        currentSetting.isFullScreen = value;
    }
}

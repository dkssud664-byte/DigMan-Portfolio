using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class SettingUI : UIPanel
{
    [SerializeField] private Button closeBtn;
    [Header("Sound")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Mouse")]
    [SerializeField] private TMP_InputField sensitivityInput;
    [SerializeField] private TextMeshProUGUI sensitivityText;

    [Header("Screen Size")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    private Resolution[] resolutions;


    private void Awake()
    {
        InitailizeCloseButton();
    }
    void Start()
    {
        //초기화
        InitailizeVolume();
        InitializeResolutionDropDown();
        InitializeSensitivityInput();
        SetSentivity();

        //스크롤바 움직이면 현재 값 저장
        masterSlider.onValueChanged.AddListener(SettingManager.Instance.SetMaster);
        bgmSlider.onValueChanged.AddListener(SettingManager.Instance.SetBGM);
        sfxSlider.onValueChanged.AddListener(SettingManager.Instance.SetSFX);
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
    
    private void InitailizeCloseButton()
    {
        //x버튼 누르면 세팅 UI 비활성화
        closeBtn.onClick.AddListener(() => {
            Facade.Instance.UIManager.CloseTop();
        });
    }


    private void InitailizeVolume()
    {
        masterSlider.value = SoundManager.Instance.master;
        bgmSlider.value = SoundManager.Instance.bgm;
        sfxSlider.value = SoundManager.Instance.sfx;
    }

    public void SetSentivity()
    {
        this.sensitivityText.text = $"Current : {SettingManager.Instance.currentSetting.sensitivity}";
    }

    private void InitializeResolutionDropDown()
    {
        //지원 해상도 목록 가져오기
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = SettingManager.Instance.currentSetting.windowDropdownIndex;

        //for(int i = resolutions.Length - 1; i >= 0; i--)
        //{
        //    string option = resolutions[i].width + " x " + resolutions[i].height;
        //    options.Add(option);

        //    if(resolutions[i].width == Screen.currentResolution.width &&
        //        resolutions[i].height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = i;
        //    }
        //}

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //드롭다운 이벤트 등록
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        //전체화면 토글 초기화
        InitializeFullScreenToggle();
    }

    public void InitializeFullScreenToggle()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    public void SetResolution(int index)
    {
        Resolution r = resolutions[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        SettingManager.Instance?.SetWindowSize(r.width, r.height, index);
        SaveLoadManager.Instance?.SaveSetting();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        SettingManager.Instance?.SetFullScreen(isFullscreen);
        SaveLoadManager.Instance?.SaveSetting();
    }

    private void InitializeSensitivityInput()
    {
        sensitivityText.text = SettingManager.Instance?.currentSetting.sensitivity.ToString();
        sensitivityInput.onSubmit.AddListener((x) =>
        {
            if (int.TryParse(x, out int result))
            {
                result = Math.Clamp(result, 1, 3000);
                SettingManager.Instance?.SetSensitivity(result);
                SaveLoadManager.Instance?.SaveSetting();
                SetSentivity();
            }
        });
    }

}

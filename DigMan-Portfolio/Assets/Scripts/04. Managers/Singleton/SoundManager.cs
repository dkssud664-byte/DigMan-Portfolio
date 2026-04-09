using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    //오디오 클립
    [System.Serializable]
    public struct BGMClipData
    {
        public Scenes type;
        public AudioClip clip;
    }
    

    public static SoundManager Instance { get; private set; }

    [Tooltip("배경음악 클립")]
    [SerializeField] private List<BGMClipData> bgmClipDatas;
    [SerializeField] private Dictionary<Scenes, AudioClip> bgmClips;   //bgm모음
    [Tooltip("배경음악 실행")]
    [SerializeField] private AudioSource audioSource;   //자식오브젝트에서 BGM 실행

    [SerializeField] private AudioMixer audioMixer;     //오디오 믹서


    #region 오디오 믹서 변수
    public float master;   //전체 불륨
    public float bgm;      //배경음악 불륨
    public float sfx;      //효과음 볼륨
    #endregion

    private void Awake()
    {
        //싱글톤
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //딕셔너리 초기화
        bgmClips = new Dictionary<Scenes, AudioClip>();
        foreach(BGMClipData clip in bgmClipDatas)
        {
            bgmClips.Add(clip.type, clip.clip);
        }
    }

    void Start()
    {
        //오디오 믹서에 쓸 변수 초기화
        if (SettingManager.Instance != null)
        {
            master = SettingManager.Instance.currentSetting.master;
            bgm = SettingManager.Instance.currentSetting.bgm;
            sfx = SettingManager.Instance.currentSetting.sfx;

            SetAudioMixerMaster();
            SetAudioMixerBGM();
            SetAudioMixerSFX();
        }
    }

    //BGM 실행
    public void PlayBGM(Scenes type)
    {
        if(!bgmClips.ContainsKey(type))
        {
            return;
        }
        audioSource.clip = bgmClips[type];
        audioSource.Play();
    }

    //오디오 믹서 변수 업데이트
    public void SetMaster(float master)
    {
        this.master = master;
        SetAudioMixerMaster();
    }

    public void SetBGM(float bgm)
    {
        this.bgm = bgm;
        SetAudioMixerBGM();
    }

    public void SetSFX(float sfx)
    {
        this.sfx = sfx;
        SetAudioMixerSFX();
    }

    public void SetAudioMixerMaster()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(Mathf.Max(master, 0.0001f)) * 20f);
    }
    public void SetAudioMixerBGM()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(Mathf.Max(bgm, 0.0001f)) * 20f);
    }
    public void SetAudioMixerSFX()
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Max(sfx, 0.0001f)) * 20f);
    }
}

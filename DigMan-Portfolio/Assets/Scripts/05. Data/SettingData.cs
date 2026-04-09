using UnityEngine;
using UnityEngine.Timeline;

[System.Serializable]
public class SettingData
{
    #region 오디오믹서 변수
    public float master;
    public float bgm;
    public float sfx;
    #endregion

    #region 윈도우 사이즈
    public int windowSizeWidth;                 //윈도우 가로
    public int windowSizeHeight;               //윈도우 세로
    public int windowDropdownIndex = 0;         //드롭다운 인덱스
    public bool isFullScreen = true;            //전체화면
    #endregion

    #region 플레이어
    public int sensitivity;               //감도
    #endregion

    public SettingData Init()
    {
        master = 1f;
        bgm = 1f;
        sfx = 1f;

        windowSizeWidth = 1920;
        windowSizeHeight = 1080;
        sensitivity = 300;

        return this;
    }
}

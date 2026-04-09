using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    #region 로드 정보
    public int loadIndex;
    public string saveTime;
    #endregion

    //public SettingData settingData; //세팅 정보
    public PlayerData playerData;   //플레이어 정보
    public MapData mapData;         //맵 정보
    public MineralSaveData mineralSaveData; //광물 위치 정보

    public GameSaveData Init()
    {
        //플레이어
        playerData = new PlayerData();
        playerData.Init();

        //맵
        mapData = new MapData();

        //광물
        mineralSaveData = new MineralSaveData();

        return this;
    }
}

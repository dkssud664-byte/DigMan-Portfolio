using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    #region 플레이어
    public Vector3 playerPosition;                      //위치
    public Quaternion playerRotation;                   //회전
    public int money;                                   //돈
    public int maxDepth;                                //최대깊이
    public int maxHp;                                   //최대 체력
    public float hp;                                    //체력
    public int maxStamina;                              //최대 스테미나
    public float stamina;                               //스테미나
    public int maxWeight;                               //최대 무게
    public float weight;                                //무게
    public float speed;                                 //이동 속도
    public float jumpPower;                             //점프 파워
    public float grivity;                               //중력
    public float jetpackPower;                          //제트팩 파워
    public List<EquipType> playerUnlockEquipData;       //해금 장비
    public List<EquipRuntimeData> equipRuntimeDatas;    //게임 진행 중 장비 정보
    public PlayerUpgradeLevel upgradeLevel;             //플레이어 업그레이드 정보
    public EquipType currentEquip;                      //현재 착용 장비
    #endregion

    //초기화
    public PlayerData Init()
    {
        playerPosition = new Vector3(0, 600, 0);
        money = 100;
        maxDepth = 0;
        grivity = -9.81f;
        currentEquip = EquipType.None;

        upgradeLevel = new PlayerUpgradeLevel();
        upgradeLevel.Initialize();

        playerUnlockEquipData = new List<EquipType>();
        equipRuntimeDatas = new List<EquipRuntimeData>();
        return this;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public float Hp { get; private set; }
    public int MaxHp { get; private set; }
    public int Money { get; private set; }
    public int MaxDepth { get; private set; }
    public int MaxStamina { get; private set; }
    public float Stamina { get; private set; }
    public float Weight { get; private set; }
    public int MaxWeight {  get; private set; }
    public float Speed { get; private set; }
    public float JumpPower { get; private set; }
    public float Gravity { get; private set; }
    public float JetpackPower { get; private set; }
    public PlayerUnlockEquipData PlayerUnlockEquipData { get; private set; }
    public Dictionary<EquipType, EquipRuntimeData> equipRuntimeData { get; private set; }
    public EquipType CurrentEquipment { get; private set; }
    [SerializeField] private PlayerStatLevelDatabase statLevelData;
    [SerializeField] private EquipmentDatabase equipmentDatabase;
    public PlayerUpgradeLevel PlayerUpgradeLevel { get; private set; }

    public event Action OnChangeMoney;
    public event Action<EquipType> OnChangeCurrentEquip;
    public event Action<EquipType, PlayerUpgradeLevel> OnChangeCurrentEquipStat;


    #region 저장 불러오기
    //세이브 데이터로 초기화
    public void InitializePlayer(GameSaveData gameSaveData)
    {
        //위치
        this.gameObject.transform.position = gameSaveData.playerData.playerPosition;
        this.gameObject.transform.rotation = gameSaveData.playerData.playerRotation;

        //업그레이드 레벨
        PlayerUpgradeLevel = gameSaveData.playerData.upgradeLevel;
        PlayerUpgradeLevel.BuildRuntimeCache();

        //속성
        MaxHp = statLevelData.Get(PlayerStatType.hp).Value[PlayerUpgradeLevel.GetStat(PlayerStatType.hp).level];
        Hp = gameSaveData.playerData.hp <= 0 ? MaxHp : gameSaveData.playerData.hp;
        Money = gameSaveData.playerData.money;
        MaxDepth = gameSaveData.playerData.maxDepth;
        MaxStamina = statLevelData.Get(PlayerStatType.stamina).Value[PlayerUpgradeLevel.GetStat(PlayerStatType.stamina).level];
        Stamina = gameSaveData.playerData.stamina <= 0 ? MaxStamina : gameSaveData.playerData.stamina;
        MaxWeight = statLevelData.Get(PlayerStatType.weight).Value[PlayerUpgradeLevel.GetStat(PlayerStatType.weight).level];
        Weight = gameSaveData.playerData.weight <= 0 ? MaxWeight : gameSaveData.playerData.weight;
        Speed = statLevelData.Get(PlayerStatType.speed).Value[PlayerUpgradeLevel.GetStat(PlayerStatType.speed).level];
        JumpPower = statLevelData.Get(PlayerStatType.jumpPower).Value[PlayerUpgradeLevel.GetStat(PlayerStatType.jumpPower).level];
        Gravity = gameSaveData.playerData.grivity;
        JetpackPower = statLevelData.Get(PlayerStatType.jetpackPower).Value[PlayerUpgradeLevel.GetStat(PlayerStatType.jetpackPower).level];
        //리스트를 해쉬셋으로 변경
        PlayerUnlockEquipData = new PlayerUnlockEquipData();
        foreach (EquipType type in gameSaveData.playerData.playerUnlockEquipData)
        {
            PlayerUnlockEquipData.unlockedEquip.Add(type);
        }

        CurrentEquipment = gameSaveData.playerData.currentEquip;
    }

    //저장하기 위해 PlayerData로 변환
    public PlayerData PlayerInfoConvertToPlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.playerPosition = this.gameObject.transform.position;
        playerData.playerRotation = this.gameObject.transform.rotation;
        playerData.maxHp = MaxHp;
        playerData.hp = Facade.Instance.PlayerManager.PlayerController.PlayerStatsSystem.CurrentHP;
        playerData.money = Money;
        playerData.maxDepth = MaxDepth;
        playerData.maxStamina = MaxStamina;
        playerData.stamina = Facade.Instance.PlayerManager.PlayerController.PlayerStatsSystem.CurrentStamina;
        playerData.maxWeight = MaxWeight;
        playerData.weight = Weight;
        playerData.speed = Speed;
        playerData.jumpPower = JumpPower;
        playerData.grivity = Gravity;
        playerData.jetpackPower = JetpackPower;

        //업그레이드 레벨 정보
        playerData.upgradeLevel = PlayerUpgradeLevel;

        //해쉬셋을 리스트로 변경
        playerData.playerUnlockEquipData =
            PlayerUnlockEquipData.unlockedEquip.ToList();

        playerData.currentEquip = CurrentEquipment;

        return playerData;
    }
    #endregion

    public bool SpendMoney(int value)
    {
        int remain = Money - value;

        if(remain < 0)
        {
            return false;
        }

        Money -= value;

        OnChangeMoney?.Invoke();

        return true;
    }

    public void RefundMoney(int value)
    {
        Money += value;
        OnChangeMoney?.Invoke();
    }

    public void UnlockEquip(EquipType type)
    {
        PlayerUnlockEquipData.unlockedEquip.Add(type);

    }

    #region 업그레이드
    //장비
    public void UpgradeLevel(EquipType equipType, EquipStatType equipStatType)
    {
        PlayerUpgradeLevel.GetEquip(equipType).GetStat(equipStatType).level++;
    }

    public void DowngradeLevel(EquipType equipType, EquipStatType equipStatType)
    {
        PlayerUpgradeLevel.GetEquip(equipType).GetStat(equipStatType).level--;
    }

    //플레이어 스텟
    public void UpgradeLevel(PlayerStatType type)
    {
        int level = ++PlayerUpgradeLevel.GetStat(type).level;
        SetPlayerStat(type, level);
    }
    public void DowngradeLevel(PlayerStatType type)
    {
        int level = --PlayerUpgradeLevel.GetStat(type).level;
        SetPlayerStat(type, level);
    }

    private void SetPlayerStat(PlayerStatType type, int level)
    {
        switch (type)
        {
            case PlayerStatType.hp:
                this.MaxHp = statLevelData.Get(type).Value[level];
                break;
            case PlayerStatType.stamina:
                this.Stamina = statLevelData.Get(type).Value[level];
                break;
            case PlayerStatType.weight:
                this.Weight = statLevelData.Get(type).Value[level];
                break;
            case PlayerStatType.speed:
                this.Speed = statLevelData.Get(type).Value[level];
                break;
            case PlayerStatType.jumpPower:
                this.JumpPower = statLevelData.Get(type).Value[level];
                break;
            case PlayerStatType.jetpackPower:
                this.JetpackPower = statLevelData.Get(type).Value[level];
                break;
            default:
                break;
        }
    }
    #endregion

    public void SetCurrentEquip(EquipType type)
    {
        //if (CurrentEquipment == type)
        //{
        //    return;
        //}

        if (CurrentEquipment == type)
        {
            CurrentEquipment = EquipType.None;
        }

        CurrentEquipment = type;
        OnChangeCurrentEquip?.Invoke(type);
        OnChangeCurrentEquipStat?.Invoke(type, PlayerUpgradeLevel);
    }
}

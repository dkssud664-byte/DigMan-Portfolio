using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class ShopCanvas : UIPanel, IInteractable
{
    [Header("Datas")]
    [SerializeField] PlayerStatLevelDatabase playerStatLevelDatabase;
    [SerializeField] EquipmentDatabase equipmentDatabase;

    [Header("Prefabs")]
    [SerializeField] GameObject statSlotPrefab;
    [SerializeField] GameObject equipSlotPrefab;

    [Header("Creat Prefab")]
    [SerializeField] RectTransform statContent;
    [SerializeField] RectTransform equipContent;
    public List<StatContentSlot> StatSlots { get; private set; } = new ();
    public List<EquipContentSlot> EquipSlots { get; private set; } = new();

    [Header("Buttons")]
    [SerializeField] Button statButton;
    [SerializeField] Button equipButton;
    [SerializeField] Button closeButton;

    [Header("Content Area")]
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] GameObject statScrollView;
    [SerializeField] GameObject equipScrollView;

    private PlayerInfo playerInfo;
    private UIManager uiManager;

    private void Awake()
    {
        playerInfo = Facade.Instance.PlayerManager.PlayerInfo;
        Init();

        MoneyUpdate();
    }


    private void OnEnable()
    {
        if(playerInfo != null)
        {
            playerInfo.OnChangeMoney += MoneyUpdate;
        }
        ScrollToTop();
        MoneyUpdate();
    }


    private void OnDisable()
    {
        playerInfo.OnChangeMoney -= MoneyUpdate;
    }

    public void Init()
    {
        InitializeStatButton();
        InitializeEquipButton();
        InitializeCloseButton();

        CreateStatSlot();
        CreateEquipSlot();
    }

    #region 버튼
    public void InitializeStatButton()
    {
        statButton.onClick.AddListener(() => {
            statButton.interactable = false;
            equipButton.interactable = true;

            statScrollView.SetActive(true);

            InitializeStatSlot();

            if (equipScrollView.activeSelf)
            {
                equipScrollView.SetActive(false);
            }
        });
    }

    public void InitializeEquipButton()
    {
        equipButton.onClick.AddListener(() => {
            equipButton.interactable = false;
            statButton.interactable = true;

            equipScrollView.SetActive(true);
            InitializeEquipSlot();

            if (statScrollView.activeSelf)
            {
                statScrollView.SetActive(false);
            }
        });
    }

    public void InitializeCloseButton()
    {
        closeButton.onClick.AddListener(() =>
        {
            uiManager.CloseTop();
        });
    }
    #endregion
   
    #region 슬롯
    public void CreateStatSlot()
    {
        foreach (PlayerStatType statType in Enum.GetValues(typeof(PlayerStatType)))
        {
            StatContentSlot statSlot = Instantiate(statSlotPrefab, statContent).
                GetComponent<StatContentSlot>();
            statSlot.SetType(statType);
            statSlot.Init();
            statSlot.OnClickPlus += HandleUpgrade;
            statSlot.OnClickMinus += HandleDowngrade;
            StatSlots.Add(statSlot);
        }
    }

    private void InitializeStatSlot()
    {
        foreach(StatContentSlot statSlot in StatSlots)
        {
            statSlot.UpdateSlot(statSlot.Type, playerStatLevelDatabase,
                playerInfo.PlayerUpgradeLevel.GetStat(statSlot.Type));
        }
    }

    public void CreateEquipSlot()
    {
       foreach(EquipType type in Enum.GetValues(typeof(EquipType)))
       { 
            if(type == EquipType.None)
            {
                continue;
            }

            EquipContentSlot equipSlot = Instantiate(equipSlotPrefab, equipContent).
                GetComponent<EquipContentSlot>();
            equipSlot.SetType(type);
            equipSlot.InitializeUiMap();

            equipSlot.OnClickUnlock += HandleUnlockEquip;

            foreach(EquipStatType statType in Enum.GetValues(typeof(EquipStatType)))
            {
                EquipStatUIGroup uiGroup = equipSlot.GetUiMap(statType);
                uiGroup.equipType = type;
                uiGroup.equipStatType = statType;
                uiGroup.Init();
                uiGroup.OnClickPlus += HandleUpgrade;
                uiGroup.OnClickMinus += HandleDowngrade;
            }
            EquipSlots.Add(equipSlot);
       }
    }

    private void InitializeEquipSlot()
    {
        foreach (EquipContentSlot equipStatSlot in EquipSlots)
        {
            equipStatSlot.UpdateSlot(equipStatSlot.Type, equipmentDatabase,
                playerInfo.PlayerUpgradeLevel,
                playerInfo.PlayerUnlockEquipData.unlockedEquip.Contains(equipStatSlot.Type));
        }
    }
    #endregion

    
    //장비 추가
    private void HandleUnlockEquip(EquipType type)
    {
        EquipmentData data = equipmentDatabase.Get(type);
        int cost = data.UnlockCost;

        if (!playerInfo.SpendMoney(cost))
        {
            Debug.Log("돈 부족");
            return;
        }

        playerInfo.UnlockEquip(type);

        Facade.Instance.PlayerManager.PlayerCanvas.
            OnExpandEquipArea(playerInfo.PlayerUnlockEquipData); //장비슬롯 추가
        InitializeEquipSlot(); // UI 갱신
        MoneyUpdate();
    }
    #region 업그레이드
    //장비
    private void HandleUpgrade(EquipType equipType, EquipStatType equipStatType)
    {
        int level = playerInfo.PlayerUpgradeLevel.GetEquip(equipType).GetStat(equipStatType).level;
        EquipmentData data = equipmentDatabase.Get(equipType);

        if(level >= data.GetStat(equipStatType).costs.Length - 1)
        {
            return;
        }

        int cost = data.GetStat(equipStatType).costs[
            playerInfo.PlayerUpgradeLevel.GetEquip(equipType).GetStat(equipStatType).level];

        if (!playerInfo.SpendMoney(cost))
        {
            Debug.Log("돈 부족");
            return;
        }
        playerInfo.UpgradeLevel(equipType, equipStatType);

        InitializeEquipSlot(); // UI 갱신
        MoneyUpdate();
    }

    private void HandleDowngrade(EquipType equipType, EquipStatType equipStatType)
    {
        int level = playerInfo.PlayerUpgradeLevel.GetEquip(equipType).GetStat(equipStatType).level;
        if (level <= 0)
        {
            return;
        }

        EquipmentData data = equipmentDatabase.Get(equipType);
        int cost = data.GetStat(equipStatType).costs[level - 1];

        playerInfo.RefundMoney(cost);
        playerInfo.DowngradeLevel(equipType, equipStatType);

        InitializeEquipSlot(); // UI 갱신
        MoneyUpdate();
    }

    //플레이어 스텟
    private void HandleUpgrade(PlayerStatType type)
    {
        int level = playerInfo.PlayerUpgradeLevel.GetStat(type).level;
        PlayerStatLevelData data = playerStatLevelDatabase.Get(type);
        if (level >= data.Cost.Length - 1)
        {
            return;
        }

        int cost = data.Cost[level];

        if(!playerInfo.SpendMoney(cost))
        {
            Debug.Log("돈 부족");
            return;
        }
        
        playerInfo.UpgradeLevel(type);
        Facade.Instance.PlayerManager.PlayerController.PlayerStatsSystem.ResetHp(type, playerInfo);
        InitializeStatSlot();
        MoneyUpdate();

    }
    private void HandleDowngrade(PlayerStatType type)
    {
        int level = playerInfo.PlayerUpgradeLevel.GetStat(type).level;
        PlayerStatLevelData data = playerStatLevelDatabase.Get(type);
        if (level <= 0)
        {
            return;
        }

        int cost = data.Cost[level - 1];

        playerInfo.RefundMoney(cost);
        playerInfo.DowngradeLevel(type);
        Facade.Instance.PlayerManager.PlayerController.PlayerStatsSystem.ResetHp(type, playerInfo);
        InitializeStatSlot();
        MoneyUpdate();
    }

    #endregion

    //스크롤 초기화
    void ScrollToTop()
    {
        ScrollRect statScrollRect = statScrollView.GetComponent<ScrollRect>();
        ScrollRect equipScrollRect = equipScrollView.GetComponent<ScrollRect>();
        statScrollRect.verticalNormalizedPosition = 1f;
        equipScrollRect.verticalNormalizedPosition = 1f;
    }

    public void MoneyUpdate()
    {
        if(playerInfo == null)
        {
            Debug.Log("없음");
            return;
        }
       

        moneyText.text = $"Money : {playerInfo.Money}$";
    }

    public void Interact()
    {
        if(uiManager == null)
        {
            uiManager = Facade.Instance.UIManager;
        }

        if(this.gameObject.activeSelf)
        {
            uiManager.CloseTop();
        }
        else
        {
            uiManager.Open(this);
        }
    }

}

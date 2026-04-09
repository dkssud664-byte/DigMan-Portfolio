using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerCanvas : MonoBehaviour
{
    [Header("data")]
    [SerializeField] private PlayerStatLevelDatabase playerStatLevelDatabase;
    [SerializeField] private EquipmentDatabase equipmentDatabase;

    [Header("Gauge Area")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Slider weightSlider;

    [Header("Equip Area")]
    [SerializeField] private RectTransform equipAreaRT;
    [SerializeField] private GameObject equipSlotPrefab;
    public Dictionary<EquipType, EquipSlot> equipSlots = new();

    [Header("Mini Map")]
    [SerializeField] private RectTransform directionRT;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI depthText;

    [Header("Interaction")]
    [SerializeField] private GameObject interactionArea;
    public GameObject InteractionArea {  get { return interactionArea; } }

    [Header("Prefabs")]
    [SerializeField] private GameObject equipSlotPrefabs;
    private int equipSlotWidth = 100;                       //장비칸 확장 가로 간격

    private PlayerInfo playerInfo;                          //플레이어 정보
    private PlayerStatsSystem playerStatsSystem;            //플레이어 시스템

    private void Awake()
    {
        playerInfo = Facade.Instance.GameSceneController.Player.GetComponent<PlayerInfo>();
        playerStatsSystem = playerInfo.GetComponent<PlayerController>().PlayerStatsSystem;
    }

    private void OnEnable()
    {
        playerStatsSystem.OnHPChanged += OnHpChanged;
        playerStatsSystem.OnStaminaChanged += OnStaminaChanged;
        playerInfo.OnChangeCurrentEquip += OnChangeCurrentEquip;

    }

    private void Start()
    {
        InitializeCanvas();
    }

   

    private void OnDisable()
    {
        playerStatsSystem.OnHPChanged -= OnHpChanged;
        playerStatsSystem.OnStaminaChanged -= OnStaminaChanged;
        playerInfo.OnChangeCurrentEquip -= OnChangeCurrentEquip;
    }

    //초기화
    public void InitializeCanvas()
    {
        UpdateSlider(hpSlider, ((float)playerInfo.Hp / (float)playerInfo.MaxHp));
        UpdateSlider(staminaSlider, (float)playerInfo.Stamina / (float)playerInfo.MaxStamina);
        UpdateSlider(weightSlider, (float)playerInfo.Weight / (float)playerInfo.MaxWeight);
        UpdateMoney(playerInfo.Money);
        UpdateDepth(playerInfo.MaxDepth);
    }

    //플레이어 정보 가져오기
    public void SetPlayerInfo(PlayerInfo playerInfo)
    {
        if(playerInfo == null)
        {
            return;
        }
        this.playerInfo = playerInfo;
    }

    #region 플레이어 게이지 UI 업데이트
    public void UpdateSlider(Slider slider, float value)
    {
        slider.value = Mathf.Clamp(value, 0, 1);
    }

    public void OnHpChanged(float value)
    {
        UpdateSlider(hpSlider, value);
    }

    public void OnStaminaChanged(float value)
    {
        UpdateSlider(staminaSlider, value);
    }

    public void OnWeightChanged(float value)
    {
        UpdateSlider(weightSlider, value);
    }
    #endregion

    #region 플레이어 Money, Depth 업데이트

    //돈 업데이트
    public void UpdateMoney(int money)
    {
        if(moneyText == null)
        {
            return;
        }

        moneyText.text = $"Money : {money}";
    }

    //깊이 업데이트
    public void UpdateDepth(int depth)
    {
        if(depthText == null)
        {
            return;
        }

        depthText.text = $"{depth}m";
    }
    #endregion

    #region 장비 추가
    //장비슬롯 범위 추가
    public void OnExpandEquipArea(PlayerUnlockEquipData data)
    {
        //총 범위를 늘린다
        equipAreaRT.sizeDelta = new Vector2(equipSlotWidth * data.unlockedEquip.Count,
            equipAreaRT.sizeDelta.y);

        //없으면 추가
        foreach(EquipType eType in data.unlockedEquip)
        {
            if (!equipSlots.ContainsKey(eType))
            {
                AddEquipSlot(eType);
            }
        }

        UpdateEquipSlot();
    }

    private void AddEquipSlot(EquipType eType)
    {
        GameObject go = Instantiate(equipSlotPrefab, equipAreaRT);
        EquipSlot equipSlot = go.GetComponent<EquipSlot>();
        equipSlots[eType] = equipSlot;
    }
    #endregion

    public void UpdateEquipSlot()
    {
        foreach(EquipType eType in equipSlots.Keys)
        {
            EquipmentData data = equipmentDatabase.Get(eType);
            if (equipSlots.ContainsKey(eType))
            {
                equipSlots[eType].UpdateUI(data.Icon, data.Quantity.ToString());
            }
        }
    }

    //미니맵 방위 표시
    public void UpdateDirectionIcon(Transform player)
    {
        if(player == null)
        {
            return;
        }
        this.directionRT.localEulerAngles = new Vector3(0, 0, -player.eulerAngles.y);
    }

    private void OnChangeCurrentEquip(EquipType type)
    {
        foreach (var slot in equipSlots)
        {
            slot.Value.SetSelected(false);
        }

        equipSlots[type].SetSelected(true);
    }
}

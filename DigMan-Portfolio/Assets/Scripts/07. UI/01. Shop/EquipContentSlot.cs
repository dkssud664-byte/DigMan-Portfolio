using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipContentSlot : MonoBehaviour
{
    public EquipType Type {get; private set;}
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI equipName;

    [Header("Stat UI")]
    [SerializeField] List<EquipStatUIEntry> equipStats;
    private Dictionary<EquipStatType, EquipStatUIGroup> uiMap = new();

    [Header("Unlock")]
    [SerializeField] private GameObject unlockPanel;
    [SerializeField] private TextMeshProUGUI unlockName;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button unlockBtton;

    public event Action<EquipType> OnClickUnlock;
    

    private bool isInitialized = false;

    private void Awake()
    {
        //InitializeUiMap();
        InitializeUnlockButton();
    }

    //equipStats를 맵핑
    public void InitializeUiMap()
    {
        if (isInitialized)
        {
            return;
        }

        foreach (EquipStatUIEntry entry in equipStats)
        {
            uiMap[entry.type] = entry.ui;
        }

        isInitialized = true;
    }

    private void InitializeUnlockButton()
    {
        unlockBtton.onClick.AddListener(() =>
        {
            OnClickUnlock?.Invoke(Type);
        });
    }

    public void SetType(EquipType type)
    {
        this.Type = type;
    }

    //상점 장비 UI 업데이트
    public void UpdateSlot(EquipType type, EquipmentDatabase database,
        PlayerUpgradeLevel upgradeLevel, bool isUnlocked)
    {
        InitializeUiMap();

        EquipmentData data = database.Get(type);
        if (data == null)
        {
            return;
        }

        costText.text = $"{data.UnlockCost} $"; 

        unlockPanel.SetActive(!isUnlocked);

        if (data.Icon != null)
        {
            image.sprite = data.Icon;
        }

        equipName.text = data.EquipName;

        //장비의 세부 스텟 업데이트
        foreach (EquipStatType statType in Enum.GetValues(typeof(EquipStatType)))
        {
            if(uiMap.ContainsKey(statType))
            {
                EquipStatUIGroup uiGroup = uiMap[statType];
                EquipStatLevel statLevel = upgradeLevel.GetEquip(type).GetStat(statType);
                EquipStatLevelData levelData = data.GetStat(statType);


                string strValue = statLevel.level >= levelData.values.Length ?
                    "Max" : levelData.values[statLevel.level].ToString();

                string strLevel = statLevel.level >= levelData.costs.Length ?
                    "Max" : statLevel.level.ToString();

                uiGroup.name.text = data.GetStat(statType).type.ToString();

                float ratio = statLevel.level <= 0 ?
                    0 :
                    (float)statLevel.level
                    / (float)(data.GetStat(statType).values.Length - 1);

                uiGroup.slider.value = ratio;

                uiGroup.statText.text = strValue;
                uiGroup.costText.text = strLevel == "Max" ? "Max" : $"{strLevel} $";
            }
        }
    }

    public EquipStatUIGroup GetUiMap(EquipStatType type)
    {
        if(!uiMap.ContainsKey(type))
        {
            return null;
        }

        return uiMap[type];
    }

}

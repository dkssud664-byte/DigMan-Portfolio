using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatContentSlot : MonoBehaviour
{
    public PlayerStatType Type { get; private set; }
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button minusButton;
    [SerializeField] private Button plusButton;

    public event Action<PlayerStatType> OnClickPlus;
    public event Action<PlayerStatType> OnClickMinus;


    public void UpdateSlot(PlayerStatType type, PlayerStatLevelDatabase database,
        PlayerStatUpgradeLevelData levelData)
    {
        PlayerStatLevelData data = database.Get(type);
        if (data.Icon != null)
        {
            image.sprite = data.Icon;
        }

        string strValue = levelData.level >= data.Cost.Length ?
                    "Max" : data.Value[levelData.level].ToString();

        string strLevel = levelData.level >= data.Cost.Length ?
                    "Max" : levelData.level.ToString();

        nameText.text = data.name;
        float ratio = (levelData.level <= 0f) ?
            0f : (float)levelData.level / (float)(data.Value.Length - 1);
        slider.value = ratio;
        statText.text = strValue;
        costText.text = strLevel;
    }

    public void SetType(PlayerStatType type)
    {
        Type = type;
    }

    public void Init()
    {
        plusButton.onClick.AddListener(() => {
            OnClickPlus?.Invoke(Type);
        });

        minusButton.onClick.AddListener(() => {
            OnClickMinus?.Invoke(Type);
        });
    }
}

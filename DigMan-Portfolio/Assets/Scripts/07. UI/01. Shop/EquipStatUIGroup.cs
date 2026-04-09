using System;
using TMPro;
using UnityEngine.UI;

[Serializable]
public class EquipStatUIGroup
{
    public EquipType equipType;
    public EquipStatType equipStatType;
    public TextMeshProUGUI name;
    public Slider slider;
    public TextMeshProUGUI statText;
    public TextMeshProUGUI costText;
    public Button plusButton;
    public Button minusButton;

    public event Action<EquipType, EquipStatType> OnClickPlus;
    public event Action<EquipType, EquipStatType> OnClickMinus;

    public void Init()
    {
        plusButton.onClick.AddListener(() =>
        {
            OnClickPlus?.Invoke(equipType, equipStatType);
        });

        minusButton.onClick.AddListener(() =>
        {
            OnClickMinus?.Invoke(equipType, equipStatType);
        });
    }
}
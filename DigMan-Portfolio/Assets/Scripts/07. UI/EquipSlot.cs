using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
    [SerializeField] private Image Outline;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textQuantity;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;

    public void UpdateUI(Sprite sprite, string text)
    {
        if (image != null)
        {
            image.sprite = sprite;
        }

        if(textQuantity != null)
        {
            textQuantity.text = text;
        }
    }

    public void SetSelected(bool selected)
    {
        if (Outline == null)
            return;

        Outline.color = selected ? selectedColor : normalColor;
    }
}

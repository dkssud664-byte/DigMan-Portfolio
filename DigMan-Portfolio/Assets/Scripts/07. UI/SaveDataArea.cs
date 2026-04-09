using TMPro;
using UnityEngine;

public class SaveDataArea : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI depthText;

    public void SetData(string date, string money, string depth)
    {
        dateText.text = date;
        moneyText.text = $"Money : {money}";
        depthText.text = $"Depth : {depth}";
    }
}

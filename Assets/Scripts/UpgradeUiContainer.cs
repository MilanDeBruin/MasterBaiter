
using TMPro;
using UnityEngine;

public class UpgradeUiContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text CurrentValueText;
    [SerializeField] private TMP_Text PriceText;

    public void UpdatePrice(string newPrice)
    {
        PriceText.text = newPrice;
    }

    public void UpdateCurrentValue(string currentValue)
    {
        CurrentValueText.text = "Current: " + currentValue;
    }
}
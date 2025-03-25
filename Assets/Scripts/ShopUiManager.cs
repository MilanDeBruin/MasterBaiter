using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUiManager : MonoBehaviour
{
    [SerializeField] private Image ShopBackground;
    [SerializeField] private GameObject ShopObject;

    [SerializeField] private UpgradeUiContainer CatchCountContainer;
    [SerializeField] private UpgradeUiContainer DepthContainer;
    [SerializeField] private UpgradeUiContainer SellContainer;

    [SerializeField] private UpgradeManager UpgradeManager;

    private void Start()
    {
        int currentStep = UpgradeManager.MaxCatchCount - 3;
        int price = UpgradeManager.CalculatePrice(currentStep);
        CatchCountContainer.UpdatePrice(price.ToString());
        CatchCountContainer.UpdateCurrentValue(UpgradeManager.MaxCatchCount.ToString());

        currentStep = Mathf.RoundToInt((UpgradeManager.MaxDepthMultiplier - 1) / 0.25f);
        price = UpgradeManager.CalculatePrice(currentStep);
        DepthContainer.UpdatePrice(price.ToString());
        DepthContainer.UpdateCurrentValue(UpgradeManager.MaxDepthMultiplier.ToString());


        currentStep = Mathf.RoundToInt((UpgradeManager.SellBonusMultiplier - 1) / 0.1f);
        price = UpgradeManager.CalculatePrice(currentStep);
        SellContainer.UpdatePrice(price.ToString());
        SellContainer.UpdateCurrentValue(UpgradeManager.SellBonusMultiplier.ToString());
    }

    public void ShowShop(bool enable)
    {
        ShopBackground.enabled = enable;
        ShopObject.SetActive(enable);
    }

    public void BuyCatchCount()
    {
        string nextPrice = UpgradeManager.UpgradeCatchCount();
        if (string.IsNullOrEmpty(nextPrice)) { return; }

        CatchCountContainer.UpdatePrice(nextPrice);
        CatchCountContainer.UpdateCurrentValue(UpgradeManager.MaxCatchCount.ToString());
    }

    public void BuyDepth()
    {
        string nextPrice = UpgradeManager.UpgradeDepthMultiplier();
        if (string.IsNullOrEmpty(nextPrice)) { return; }

        DepthContainer.UpdatePrice(nextPrice);
        DepthContainer.UpdateCurrentValue(UpgradeManager.MaxDepthMultiplier.ToString());
    }

    public void BuySell()
    {
        string nextPrice = UpgradeManager.UpgradeSellMultiplier();
        if (string.IsNullOrEmpty(nextPrice)) { return; }

        SellContainer.UpdatePrice(nextPrice);
        SellContainer.UpdateCurrentValue(UpgradeManager.SellBonusMultiplier.ToString());
    }

}

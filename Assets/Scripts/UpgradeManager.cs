using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private const int DefaultPrice = 100;

    public int MaxCatchCount { get; private set; } = 3;
    public float MaxDepthMultiplier { get; private set; } = 1;
    public float SellBonusMultiplier { get; private set; } = 1;

    [SerializeField] private GameManager manager;

    void Awake()
    {
        // Laad eerder opgeslagen upgrades
        MaxCatchCount = PlayerPrefs.GetInt("MaxCatchCount", 3);
        MaxDepthMultiplier = PlayerPrefs.GetFloat("MaxDepthMultiplier", 1);
        SellBonusMultiplier = PlayerPrefs.GetFloat("SellBonusMultiplier", 1);
    }

    // Prijsberekening op basis van upgrade-niveau
    public int CalculatePrice(int currentLevel)
    {
        return DefaultPrice + (DefaultPrice * currentLevel);
    }

    public string UpgradeCatchCount()
    {
        int currentStep = MaxCatchCount - 3;
        int price = CalculatePrice(currentStep);

        if (price > manager.CurrentMoneyAmount)
        {
            return string.Empty;
        }

        MaxCatchCount += 1;
        manager.RemoveMoney(price);

        return CalculatePrice(MaxCatchCount - 3).ToString();
    }

    public string UpgradeDepthMultiplier()
    {
        int currentStep = Mathf.RoundToInt((MaxDepthMultiplier - 1) / 0.25f);
        int price = CalculatePrice(currentStep);

        if (price > manager.CurrentMoneyAmount)
        {
            return string.Empty;
        }

        MaxDepthMultiplier += 0.25f;
        manager.RemoveMoney(price);

        return CalculatePrice(Mathf.RoundToInt((MaxDepthMultiplier - 1) / 0.25f)).ToString();
    }

    public string UpgradeSellMultiplier()
    {
        int currentStep = Mathf.RoundToInt((SellBonusMultiplier - 1) / 0.1f);
        int price = CalculatePrice(currentStep);

        if (price > manager.CurrentMoneyAmount)
        {
            return string.Empty;
        }

        SellBonusMultiplier += 0.1f;
        manager.RemoveMoney(price);

        return CalculatePrice(Mathf.RoundToInt((SellBonusMultiplier - 1) / 0.1f)).ToString();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("MaxCatchCount", MaxCatchCount);
        PlayerPrefs.SetFloat("MaxDepthMultiplier", MaxDepthMultiplier);
        PlayerPrefs.SetFloat("SellBonusMultiplier", SellBonusMultiplier);
        PlayerPrefs.Save();
    }
}





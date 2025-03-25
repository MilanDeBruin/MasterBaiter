using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HookManager hookManager;
    [SerializeField] private MapGenerator mapGenerator;

    [SerializeField] private PowerBar PowerBarManager;
    [SerializeField] private SoundEffectManager SoundManager;

    [SerializeField] private UpgradeManager upgradeManager;

    [SerializeField] private TMP_Text MoneyText;
    public int CurrentMoneyAmount { get; private set; }
    private int displayedMoney = 0;

    private void Start()
    {
        CurrentMoneyAmount = PlayerPrefs.GetInt("Money", 0);
        displayedMoney = CurrentMoneyAmount;
        UpdateMoneyText();
    }

    void Update()
    {
        PowerBarManager.SetEnabled(hookManager.HookReadyToCast);

        if (Input.GetKeyDown(KeyCode.F1))
        {
            AddMoney(1000);
        }
    }

    public void ThrowHook(float power)
    {
        SoundManager.InvokeCast();

        power = power * upgradeManager.MaxDepthMultiplier;

        mapGenerator.GenerateMap(Mathf.CeilToInt(power));
        hookManager.SetHookDescendTransform(new Vector3(0, -power, 0));
        hookManager.SetHookDescending();

    }

    public void GameOver()
    {
        mapGenerator.ClearMap();
    }

    public void AddMoney(int amount)
    {
        amount = Mathf.FloorToInt((float)amount * upgradeManager.SellBonusMultiplier);
        CurrentMoneyAmount += amount;
        StartCoroutine(SmoothMoneyUpdate());
    }

    public void RemoveMoney(int amount)
    {
        CurrentMoneyAmount -= amount;
        StartCoroutine(SmoothMoneyUpdate());
    }

    private IEnumerator SmoothMoneyUpdate()
    {
        float duration = 0.5f; // Tijd in seconden om te animeren
        float elapsedTime = 0f;
        int startMoney = displayedMoney;
        int targetMoney = CurrentMoneyAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            displayedMoney = Mathf.RoundToInt(Mathf.Lerp(startMoney, targetMoney, elapsedTime / duration));
            UpdateMoneyText();
            yield return null;
        }

        // Zorg dat het eindbedrag exact klopt
        displayedMoney = targetMoney;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        MoneyText.text = displayedMoney.ToString();
    }

    public UpgradeManager GetUpgradeManager()
    {
        return upgradeManager;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Money", CurrentMoneyAmount);
        PlayerPrefs.Save();
    }
}

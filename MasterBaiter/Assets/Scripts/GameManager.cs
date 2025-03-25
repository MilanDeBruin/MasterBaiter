using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private HookManager hookManager;
    [SerializeField] private MapGenerator mapGenerator;

    [SerializeField] private PowerBar PowerBarManager;
    [SerializeField] private SoundEffectManager SoundManager;

    [SerializeField] private TMP_Text MoneyText;
    private int CurrentMoneyAmount;
    public int MaxCatchCount { get; private set; } = 3;

    private void Start()
    {
        CurrentMoneyAmount = PlayerPrefs.GetInt("Money", 0);
        MaxCatchCount = PlayerPrefs.GetInt("MaxCatchCount", 3);

        UpdateMoneyText();
    }

    void Update()
    {
        PowerBarManager.SetEnabled(hookManager.HookReadyToCast);
    }

    public void ThrowHook(float power)
    {
        SoundManager.InvokeCast();

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
        CurrentMoneyAmount += amount;
        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        MoneyText.text = CurrentMoneyAmount.ToString();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Money", CurrentMoneyAmount);
        PlayerPrefs.SetInt("MaxCatchCount", MaxCatchCount);

    }
}

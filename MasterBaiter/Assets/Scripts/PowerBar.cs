using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Voor de button interactie events

public class PowerBar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameManager gameManager;

    public GameObject PowerBarParent;
    public Slider powerSlider; // Verbind de slider in de Inspector
    public TMP_Text powerText; // Verbind de Text voor de kracht in de Inspector
    public float maxPower = 100f; // Maximaal bereik voor de kracht
    public float fluctuationSpeed = 5f; // Snelheid van de fluctuatie
    public float powerChangeSpeed = 1f; // Hoe snel de kracht fluctueert

    private bool isCharging = false;
    private float currentPower = 0f;
    private float chargeTime = 0f;
    private bool overshot = false;

    private float perfectZone = 85f;
    private float overshootPenalty = 0.8f;

    public void SetEnabled(bool enabled)
    {
        if(!PowerBarParent.activeSelf && enabled)
        {
            currentPower = 50;
            powerSlider.value = currentPower / maxPower;
            powerText.text = Mathf.RoundToInt(currentPower).ToString();
        }

        PowerBarParent.SetActive(enabled);
    }

    void Update()
    {
        if (isCharging)
        {
            float fluctuation = Mathf.Sin(chargeTime * fluctuationSpeed);
            currentPower = Mathf.Clamp((fluctuation + 1) * (maxPower / 2), 0, maxPower);

            powerSlider.value = currentPower / maxPower;

            powerText.text = Mathf.RoundToInt(currentPower).ToString();

            chargeTime += Time.deltaTime * powerChangeSpeed;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isCharging)
        {
            isCharging = true;
            chargeTime = 0f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isCharging)
        {
            isCharging = false;
            DeterminePowerOutcome(currentPower);
        }
    }

    void DeterminePowerOutcome(float power)
    {
        if (power >= perfectZone && power <= maxPower)
        {
            power *= 1.2f;
        }
        else if (power > maxPower)
        {
            power *= overshootPenalty;
            overshot = true;
        }
        
        ThrowHook(power);
    }

    void ThrowHook(float power)
    {
        power = power * 10;
        gameManager.ThrowHook(power);
    }
}

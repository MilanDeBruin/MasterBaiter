
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Transform HookTransform;
    [SerializeField] private Transform HookInitTransform;

    private Vector3 HookDescendTransform;
    public bool HookIsDescending { get; private set; } = false;
    public bool HookReadyToCast { get; private set; } = true;

    private List<FishManager> CaughtFish = new List<FishManager>();

    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private void ResetHook()
    {
        int totalCaught = 0;
        foreach (var fish in CaughtFish)
        {
            totalCaught += fish.GetFishValue();
        }

        gameManager.AddMoney(totalCaught);

        CaughtFish.Clear();
        gameManager.GameOver();

        HookReadyToCast = true;
    }

    void Update()
    {
        if(HookIsDescending)
        {
            if (HookTransform.transform.position.y < HookDescendTransform.y + 1f)
            {
                HookIsDescending = false;
            }
            else
            {
                HookTransform.position = Vector3.Lerp(
                        HookTransform.position,
                        new Vector3(HookInitTransform.position.x, HookDescendTransform.y, HookInitTransform.position.z),
                        Time.deltaTime * 2f // Pas de snelheid aan
                    );
            }
            return;
        }


        if (HookTransform.transform.position.y != 0 && !HookIsDescending)
        {
            if(HookTransform.localPosition.y > -0.5f)
            {
                HookTransform.transform.position = HookInitTransform.position;
                ResetHook();

            }
            else if(HookTransform.transform.position.y > -125 || CaughtFish.Count >= gameManager.GetUpgradeManager().MaxCatchCount)
            {
                HookTransform.position = Vector3.Lerp(
                           HookTransform.position,
                           new Vector3(HookInitTransform.position.x, HookInitTransform.position.y, HookInitTransform.position.z),
                           Time.deltaTime * 3f // Pas de snelheid aan
                       );
            }
            else
            {
                float reelSpeed = Time.deltaTime * 155f;
                if (Input.GetMouseButton(0))
                {
                    reelSpeed = reelSpeed * 3;
                }
                    
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float mouseX = mouseWorldPosition.x;
                float clampedX = Mathf.Clamp(mouseX, -130f, 130f);

                // Huidige positie van de haak
                Vector3 currentPosition = HookTransform.position;

                // Doelpositie van de haak op de x-as
                float targetX = clampedX;

                // Doelpositie van de haak op de y-as
                float targetY = currentPosition.y + reelSpeed;

                // Gebruik Vector3.SmoothDamp voor de x-as
                float smoothedX = Mathf.SmoothDamp(currentPosition.x, targetX, ref velocity.x, smoothTime);

                // Gebruik de huidige y-positie en voeg de y-beweging toe
                float newY = targetY;

                // Update de positie van de haak
                HookTransform.position = new Vector3(smoothedX, newY, currentPosition.z);
            }
            return;
        }
    }

    public void SetHookDescending() 
    {
        HookIsDescending = true;
        HookReadyToCast = false;
    }

    public void SetHookDescendTransform(Vector3 descendTransform)
    {
        HookDescendTransform = descendTransform;
    }

    public void FishCaught(FishManager fish)
    {
        CaughtFish.Add(fish);
    }
}

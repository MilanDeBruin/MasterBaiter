
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
            if(HookTransform.transform.position.y > -0.5f)
            {
                HookTransform.transform.position = HookInitTransform.position;
                ResetHook();

            }
            else if(HookTransform.transform.position.y > -125 || CaughtFish.Count >= gameManager.MaxCatchCount)
            {
                HookTransform.position = Vector3.Lerp(
                           HookTransform.position,
                           new Vector3(HookInitTransform.position.x, HookInitTransform.position.y, HookInitTransform.position.z),
                           Time.deltaTime * 3f // Pas de snelheid aan
                       );
            }
            else
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float mouseX = mouseWorldPosition.x;
                float clampedX = Mathf.Clamp(mouseX, -125f, 125f);

                HookTransform.position = new Vector3(
                       clampedX,
                       HookTransform.position.y + Time.deltaTime * 215f,
                       HookTransform.position.z
                   );

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

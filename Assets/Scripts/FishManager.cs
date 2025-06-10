using UnityEngine;

public class FishManager : MonoBehaviour
{
    public enum FishType
    {
        Clownfish,
        Salmon,
        Tuna,
        Shark,
        Goldfish,
        Angelfish,
        Pufferfish,
        Swordfish,
        Catfish
    }

    [SerializeField] private FishType fishType;
    [SerializeField] private int FishValue = 1;
    [SerializeField] private float speed = 25f;
    [SerializeField] private float changeDirectionInterval = 3f;
    private float direction = 1f;
    private float targetX;

    private Transform hookTransform;

    void Start()
    {
        targetX = Random.Range(-125f, 125f);
        InvokeRepeating("ChangeDirection", changeDirectionInterval, changeDirectionInterval);
    }

    void Update()
    {
        if (hookTransform != null)
        {
            transform.position = hookTransform.position;
            return;
        }

        switch (fishType)
        {
            case FishType.Clownfish:
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime),
                    transform.position.y,
                    transform.position.z
                );
                break;
            case FishType.Salmon:
                // Uniek gedrag voor Salmon
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime),
                    Mathf.Sin(Time.time) * 2,
                    transform.position.z
                );
                break;
            case FishType.Tuna:
                // Uniek gedrag voor Tuna
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime),
                    transform.position.y + Mathf.Sin(Time.time) * 0.5f,
                    transform.position.z
                );
                break;
            case FishType.Shark:
                // Uniek gedrag voor Shark
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime * 1.5f),
                    transform.position.y,
                    transform.position.z
                );
                break;
            case FishType.Goldfish:
                // Uniek gedrag voor Goldfish
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime * 0.5f),
                    Mathf.PingPong(Time.time, 1),
                    transform.position.z
                );
                break;
            case FishType.Angelfish:
                // Uniek gedrag voor Angelfish
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime),
                    Mathf.Cos(Time.time) * 1.5f,
                    transform.position.z
                );
                break;
            case FishType.Pufferfish:
                // Uniek gedrag voor Pufferfish
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime * 0.8f),
                    transform.position.y + Mathf.Sin(Time.time) * 0.3f,
                    transform.position.z
                );
                break;
            case FishType.Swordfish:
                // Uniek gedrag voor Swordfish
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime * 2f),
                    transform.position.y,
                    transform.position.z
                );
                break;
            case FishType.Catfish:
                // Uniek gedrag voor Catfish
                transform.position = new Vector3(
                    Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime * 0.7f),
                    transform.position.y - Mathf.Abs(Mathf.Sin(Time.time)) * 0.1f,
                    transform.position.z
                );
                break;
        }

        if (transform.position.x == targetX)
        {
            ChangeTargetPosition();
        }

        if (targetX > transform.position.x)
        {
            FlipSprite(false);
        }
        else if (targetX < transform.position.x)
        {
            FlipSprite(true);
        }
    }

    void ChangeDirection()
    {
        direction = (Random.Range(0f, 1f) > 0.5f) ? 1f : -1f;
    }

    void ChangeTargetPosition()
    {
        targetX = Mathf.Clamp(transform.position.x + (Random.Range(0f, 1f) > 0.5f ? 1 : -1) * Random.Range(10f, 50f), -125f, 125f);
    }

    void FlipSprite(bool facingRight)
    {
        Vector3 localScale = transform.localScale;
        localScale.x = facingRight ? Mathf.Abs(localScale.x) : -Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    public int GetFishValue()
    {
        return FishValue;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<HookManager>() != null)
        {
            HookManager hook = other.GetComponent<HookManager>();
            if (hook.HookIsDescending)
            {
                return;
            }
            hookTransform = other.transform;
            hook.FishCaught(this);

            SoundEffectManager.Instance.InvokeCaught();
        }
    }
}

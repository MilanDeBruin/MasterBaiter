using UnityEngine;

public class FishManager : MonoBehaviour
{
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
        }


        transform.position = new Vector3(
            Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime),
            transform.position.y,
            transform.position.z
        );

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

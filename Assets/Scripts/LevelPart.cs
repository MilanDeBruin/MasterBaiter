using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private Collider2D MiddlePart;
    [SerializeField] private GameObject[] FishPrefab;

    private const int fishPerPart = 3;

    public void Initial(int depthPart)
    {
        SpawnFishes(depthPart);
    }


    private void SpawnFishes(int depth)
    {
        Collider2D middlePartCollider = MiddlePart;

        if (middlePartCollider == null)
        {
            Debug.LogError("MiddlePart heeft geen Collider!");
            return;
        }

        Vector3 partCenter = middlePartCollider.bounds.center;
        Vector3 partSize = middlePartCollider.bounds.size;

        for (int i = 0; i < fishPerPart; i++)
        {
            float randomX = Random.Range(partCenter.x - partSize.x / 2f, partCenter.x + partSize.x / 2f);
            float randomY = Random.Range(partCenter.y - partSize.y / 2f, partCenter.y + partSize.y / 2f);
            float randomZ = Random.Range(partCenter.z - partSize.z / 2f, partCenter.z + partSize.z / 2f);
            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

            GameObject selectedFish = GetFishBasedOnDepth(depth);

            Instantiate(selectedFish, randomPosition, Quaternion.identity, MiddlePart.transform);
        }
    }

    private GameObject GetFishBasedOnDepth(int depth)
    {
        depth = depth * 300;
        int fishTier = Mathf.Min(depth / 1000, FishPrefab.Length - 1);

        float[] chances = new float[FishPrefab.Length];

        chances[fishTier] = 0.35f;

        float lowerChance = 0.65f / fishTier;
        for (int i = 0; i < fishTier; i++)
        {
            chances[i] = lowerChance;
        }

        float higherChance = 0.0005f / (FishPrefab.Length - fishTier - 1); // Verdeel de kans gelijk over de hogere tiers
        for (int i = fishTier + 1; i < FishPrefab.Length; i++)
        {
            chances[i] = higherChance;
        }

        float totalChance = 0f;
        foreach (float chance in chances) totalChance += chance;

        float randomValue = Random.Range(0, totalChance);
        float cumulative = 0f;

        for (int i = 0; i < FishPrefab.Length; i++)
        {
            cumulative += chances[i];
            if (randomValue <= cumulative)
            {
                return FishPrefab[i];
            }
        }

        return FishPrefab[0];
    }

}

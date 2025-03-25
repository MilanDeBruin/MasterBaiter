using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private Collider2D MiddlePart;
    [SerializeField] private GameObject[] FishPrefab;

    private const int fishPerPart = 4;

    public void Initial(int depthPart)
    {
        SpawnFishes();
    }


    private void SpawnFishes()
    {
        // Haal de bounds van de MiddlePart op (stel dat deze een Collider heeft)
        Collider2D middlePartCollider = MiddlePart;

        if (middlePartCollider != null)
        {
            // Het is handig om de bounds van de collider te gebruiken
            Vector3 partCenter = middlePartCollider.bounds.center;
            Vector3 partSize = middlePartCollider.bounds.size;

            for (int i = 1; i <= fishPerPart; i++)
            {
                // Willekeurige x, y en z posities binnen de bounds
                float randomX = Random.Range(partCenter.x - partSize.x / 2f, partCenter.x + partSize.x / 2f);
                float randomY = Random.Range(partCenter.y - partSize.y / 2f, partCenter.y + partSize.y / 2f);
                float randomZ = Random.Range(partCenter.z - partSize.z / 2f, partCenter.z + partSize.z / 2f);

                // Creëer een nieuwe vis op een willekeurige positie
                Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
                Instantiate(FishPrefab[Random.Range(0, FishPrefab.Length)], randomPosition, Quaternion.identity, MiddlePart.transform);
            }
        }
        else
        {
            Debug.LogError("MiddlePart heeft geen Collider!");
        }
    }
}

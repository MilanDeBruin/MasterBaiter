using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform MapPartsParent;
    [SerializeField] private GameObject MapParts;

    public void ClearMap()
    {
        foreach (Transform child in MapPartsParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void GenerateMap(int maxDepth)
    {
        float depthStep = maxDepth / 300f;

        for (int i = -1; i < depthStep; i++)
        {
            GameObject newPart = Instantiate(MapParts, new Vector3(), Quaternion.identity, MapPartsParent);
            newPart.name = "MapPart_" + i;

            newPart.GetComponent<LevelPart>().Initial(i);
        }
    }

}

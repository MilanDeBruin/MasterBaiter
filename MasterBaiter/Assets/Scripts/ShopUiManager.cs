using UnityEngine;
using UnityEngine.UI;

public class ShopUiManager : MonoBehaviour
{
    [SerializeField] private Image ShopBackground;
    [SerializeField] private GameObject ShopObject;

    public void ShowShop(bool enable)
    {
        ShopBackground.enabled = enable;
        ShopObject.SetActive(enable);
    }
}
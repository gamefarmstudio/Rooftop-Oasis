using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AttemptPurchase(string itemName, int price, GameObject prefabToGive)
    {
        if (prefabToGive == null)
        {
            Debug.LogError("Shop: " + itemName + " is missing its 3D Prefab!");
            return;
        }

        // 2. Try to spend the money
        if (ResourceManager.instance.SpendCoins(price))
        {
            Debug.Log("💰 Successfully bought " + itemName + " for " + price + " coins!");

            if (PlacementManager.instance != null)
            {
                PlacementManager.instance.SelectPlant(prefabToGive);
            }
        }
        else
        {
            Debug.LogWarning("❌ Not enough coins for " + itemName + "!");
        }
    }
}
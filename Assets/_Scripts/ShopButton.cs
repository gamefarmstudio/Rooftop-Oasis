using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [Header("What does this button sell?")]
    public string itemName = "New Plant";
    public int price = 50;
    public GameObject plantPrefab;

    public void OnClickBuy()
    {
        if (ShopManager.instance != null)
        {
            ShopManager.instance.AttemptPurchase(itemName, price, plantPrefab);
        }
        else
        {
            Debug.LogError("ShopManager is missing from the scene!");
        }
    }
}
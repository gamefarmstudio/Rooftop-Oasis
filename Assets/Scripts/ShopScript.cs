using UnityEngine;

public class ShopScript : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject closeButton;
    [HideInInspector]public static bool isShopOpen = false;
    [SerializeField] private InventoryManager InventoryManager;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isShopOpen)
            {
                shopPanel.SetActive(true);
                isShopOpen = true;
                shopButton.SetActive(false);
                closeButton.SetActive(true);
            }
            else
            {
                
                shopPanel.SetActive(false);
                isShopOpen = false;
                shopButton.SetActive(true);
                closeButton.SetActive(false);
            }
        }
         
    }
    public void OnShopIconClicked()
    {
        shopPanel.SetActive(true);
        isShopOpen = true;
        shopButton.SetActive(false);
        closeButton.SetActive(true);
    }
    public void OnCloseButtonClicked()
    {
        
        shopPanel.SetActive(false);
        isShopOpen = false;
        shopButton.SetActive(true);
        closeButton.SetActive(false);
    }
}

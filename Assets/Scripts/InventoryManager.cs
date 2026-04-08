using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Player Settings")]
    public int playerMoney = 1000;
    public GameObject inventoryPanel;
    public GameObject confirmModal;

    private GameObject selectedPlantButton;
    private int selectedCost;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SelectPlant(GameObject btn, int cost)
    {
        selectedPlantButton = btn;
        selectedCost = cost;
        Debug.Log("Selected: " + btn.name);
    }

    public void AskToBuySelected()
    {
        if (selectedPlantButton != null)
        {
            confirmModal.SetActive(true);
        }
    }

    public void ConfirmPurchase()
    {
        if (playerMoney >= selectedCost)
        {
            playerMoney -= selectedCost;

            // Switch the plant to 'Owned' mode
            InventorySlot slot = selectedPlantButton.GetComponent<InventorySlot>();
            if (slot != null) slot.MarkAsOwned();

            // Move to inventory and hide the popup
            selectedPlantButton.transform.SetParent(inventoryPanel.transform, false);
            confirmModal.SetActive(false);
            selectedPlantButton = null;
        }
    }

    public void CancelPurchase()
    {
        confirmModal.SetActive(false);
    }
}
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Connections")]
    [SerializeField] private Image iconImage;       // Drag 'icon' object here
    [SerializeField] private TextMeshProUGUI nameText; // Drag 'name' object here

    [Header("Plant Data")]
    [SerializeField] private PlantInfo myPlantInfo;
    [SerializeField] private GameObject my3DPlant;

    private Toggle myToggle;
    private bool isOwned = false;

    private void Start()
    {
        // THIS LOG MUST APPEAR IN THE CONSOLE
        Debug.Log("--- 🚨 SCRIPT WAKING UP ON: " + gameObject.name + " 🚨 ---");

        UpdateUI();

        myToggle = GetComponent<Toggle>();
        if (myToggle != null)
        {
            myToggle.onValueChanged.RemoveAllListeners();
            myToggle.onValueChanged.AddListener(delegate { OnToggleSelected(); });
        }
        else
        {
            Debug.LogError("🚨 ERROR: No Toggle component found on " + gameObject.name);
        }
    }

    public void UpdateUI()
    {
        if (myPlantInfo != null)
        {
            if (iconImage != null && myPlantInfo.plantIcon != null)
            {
                iconImage.sprite = myPlantInfo.plantIcon;
                // Force the icon to be visible!
                Debug.Log("Updating UI for " + gameObject.name + " with icon: " + myPlantInfo.plantIcon.name);
                iconImage.gameObject.SetActive(true);
                iconImage.enabled = true;
            }
            else
            {
                Debug.LogWarning("Icon Image or Plant Icon is not assigned for " + gameObject.name);
            }
            if (nameText != null)
            {
                nameText.text = myPlantInfo.plantName;
            }
            else
            {
                Debug.LogWarning("Name Text is not assigned for " + gameObject.name);
            }
        }
        else        {
            Debug.LogWarning("PlantInfo is not assigned for " + gameObject.name);
        }
    }

    public void OnToggleSelected()
    {
        // Only run if the toggle is turned ON
        if (myToggle != null && myToggle.isOn)
        {
            if (!isOwned)
            {
                InventoryManager.instance.SelectPlant(this.gameObject, myPlantInfo.plantPrice);
            }
            else
            {
                SpawnPlantOnRoof();
            }
        }
    }

    private void SpawnPlantOnRoof()
    {
        if (my3DPlant != null)
        {
            my3DPlant.SetActive(true);
            GameObject spawnPoint = GameObject.Find("PlantSpawnPoint");
            if (spawnPoint != null)
            {
                my3DPlant.transform.position = spawnPoint.transform.position;
            }
        }
    }

    public void MarkAsOwned()
    {
        isOwned = true;
    }
}
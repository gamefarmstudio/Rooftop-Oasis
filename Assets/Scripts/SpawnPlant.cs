using UnityEngine;

public class SpawnPlant : MonoBehaviour
{
    public GameObject plantPrefab1;
    public GameObject plantPrefab2;
    public Transform spawnPoint;
    public PlantSelectionManager selectionManager;

    public void Spawn()
    {
        int selected = selectionManager.GetSelectedPlantID();

        if (selected == 1)
        {
            Instantiate(plantPrefab1, spawnPoint.position, spawnPoint.rotation);
        }
        else if (selected == 2)
        {
            Instantiate(plantPrefab2, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("No plant selected!");
        }
    }
}
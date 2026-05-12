using UnityEngine;
using UnityEngine.InputSystem;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private InputAction placeObjectAction;
    [SerializeField] private Transform cursor;
    public static PlacementManager instance;

    private void Awake()
    {
        // This hooks it up when the game starts
        if (instance == null) instance = this;
    }
    void OnEnable()
    {
        placeObjectAction.Enable();
        placeObjectAction.performed += OnPlaceObject;
    }

    void OnDisable()
    {
        placeObjectAction.Disable();
        placeObjectAction.performed -= OnPlaceObject;
    }

    private void OnPlaceObject(InputAction.CallbackContext context)
    {
        
        
        if (objectToPlace == null)
        {
            Debug.LogWarning("Placement canceled: You haven't selected a plant to place yet!");
            return;
        }

        if (cursor == null)
        {
            Debug.LogWarning("Placement canceled: The placement cursor is missing!");
            return;
        }

        if (Grid_Snapping.IsOccupied)
        {
            string obstacleName = "an unknown object";
            if (Grid_Snapping.collidegameObject != null)
            {
                obstacleName = Grid_Snapping.collidegameObject.name;
            }

            Debug.LogWarning("Placement canceled: Space is blocked by " + obstacleName);
            return;
        }
        if (objectToPlace != null && cursor != null && !Grid_Snapping.IsOccupied && Grid_Snapping.collidegameObject == null)
        {
            Instantiate(objectToPlace, cursor.position, Quaternion.identity);
        }



    }

    public void SelectPlant(GameObject newPlantPrefab)
    {
        objectToPlace = newPlantPrefab;
        Debug.Log("🌱 Plant Selected: " + newPlantPrefab.name + ". Ready to place!");
    }
}
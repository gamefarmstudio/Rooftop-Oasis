using Unity.VisualScripting;
using UnityEngine;

public class Grid_Snapping : MonoBehaviour
{
    public float startX = 4.5821f;
    public float startZ = -2.99f;
    public float cellSize = 0.5f;
    public Vector3 offset = new Vector3(0.25f, 0f, 0.25f);
    public LayerMask floorLayer;
    public static bool IsOccupied;
    public static GameObject collidegameObject;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, floorLayer))
        {
            int xIndex = Mathf.FloorToInt((hit.point.x - startX) / cellSize);
            int zIndex = Mathf.FloorToInt((hit.point.z - startZ) / cellSize);

            float snappedX = startX + (xIndex * cellSize) + (cellSize / 2f);
            float snappedZ = startZ + (zIndex * cellSize) + (cellSize / 2f);

            transform.position = new Vector3(snappedX, hit.point.y, snappedZ) + offset;
        }
        if (Physics.Raycast(ray, out RaycastHit hit2, 100f))
        {
            if (hit2.collider.gameObject.CompareTag("ground"))
            {
                IsOccupied = false;
            }
            else
            { 
                IsOccupied = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        Debug.Log("🚨 CURSOR TOUCHED: " + other.gameObject.name + " (Layer: " + LayerMask.LayerToName(other.gameObject.layer) + ")");

        if (((1 << other.gameObject.layer) & floorLayer) == 0)
        {
            collidegameObject = other.gameObject;
            IsOccupied = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (((1 << other.gameObject.layer) & floorLayer) == 0)
        {
            collidegameObject = other.gameObject;
            IsOccupied = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & floorLayer) == 0)
        {
            collidegameObject = null;
            IsOccupied = false;
        }
    }

}







using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{
    public Camera mainCamera;
    [SerializeField] private LayerMask PickUpLayer;
    [SerializeField] private Transform cursor;

    private ObjectGrabbable heldObject;

    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    private void Update()
    {
        // 1. Create a Ray from the Mouse Position into the 3D world
        Ray mouseRay = new Ray(cursor.position, cursor.forward);

        // 2. Left Click to Pick Up or Drop
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObject == null) // If we aren't holding anything, try to pick up
            {
                // We use 100f for distance since the isometric camera is far back
                if (Physics.Raycast(mouseRay, out RaycastHit hit, 100f, PickUpLayer))
                {
                    if (hit.transform.TryGetComponent(out heldObject))
                    {
                        heldObject.Grab();
                    }
                }
            }
            else // If we ARE holding something, drop it
            {
                heldObject.Drop();
                heldObject = null;
            }
        }

        // 3. Make the held object follow the mouse
        if (heldObject != null)
        {
            // Create an invisible math plane floating 1 unit above the ground
            Plane groundPlane = new Plane(Vector3.up, Vector3.up * 1f);

            // Find where the mouse ray intersects with this floating plane
            if (groundPlane.Raycast(mouseRay, out float distance))
            {
                Vector3 hoverPosition = mouseRay.GetPoint(distance);
                heldObject.UpdateHoverPosition(hoverPosition);
            }
        }
    }
}
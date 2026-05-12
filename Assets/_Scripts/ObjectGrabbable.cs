using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    public Rigidbody objectRigidbody;

    private Vector3 targetPosition;
    [HideInInspector] public bool isGrabbed = false;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();

        // We ensure it starts with normal gravity
        objectRigidbody.useGravity = true;
        
    }

    public void Grab()
    {
        isGrabbed = true;
        objectRigidbody.useGravity = false;

        // Freeze all rotations so it doesn't spin wildly while dragging
        objectRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Drop()
    {
        isGrabbed = false;
        objectRigidbody.useGravity = true;

        // Unfreeze X and Z rotation if you want it to be able to tip over naturally, 
        // or keep FreezeRotation active if you want plants to always stand perfectly straight up.
        objectRigidbody.constraints = RigidbodyConstraints.None;
    }

    public void UpdateHoverPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    private void FixedUpdate()
    {
        if (isGrabbed)
        {
            // Smoothly glide to the mouse pointer using Rigidbody movement
            Vector3 newPosition = Vector3.Lerp(objectRigidbody.position, targetPosition, Time.fixedDeltaTime * 15f);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}
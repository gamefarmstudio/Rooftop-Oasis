using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public float panSpeed = 10f;
    public float smoothTime = 0.15f; // Higher = "floatier" camera

    private Vector3 targetPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        // Set the starting target to wherever you placed the camera in the Scene
        targetPosition = transform.position;
    }

    void Update()
    {
        // 1. Get WASD Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // 2. Calculate direction based on the camera's angle
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Flatten out the Y axis so WASD slides the camera flat across the room
        forward.y = 0;
        right.y = 0;
        forward.Normalize(); // Capital 'N'!
        right.Normalize();

        Vector3 moveDirection = (forward * moveZ + right * moveX).normalized;

        // 3. Move the target position
        targetPosition += moveDirection * panSpeed * Time.deltaTime;

        // 4. Smoothly glide the camera to the target
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform cam;
    public float moveSpeed = 8f;
    [Range(1f, 25f)] public float rotationSpeed = 10f;

    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (cam == null) cam = Camera.main.transform;
    }

    void Update()
    {
        // 1. ONLY gather input and calculate direction in Update
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 forward = cam.forward;
        Vector3 right = cam.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        moveDirection = (forward * moveZ + right * moveX).normalized;
    }

    void FixedUpdate()
    {
        // 2. Apply Physics Velocity
        Vector3 velocity = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            // RotateTowards is mathematically bulletproof for perfect 180-degree backwards flips.
            // We multiply rotationSpeed by 50 to convert it to degrees-per-second so it feels similar.
            float turnSpeed = rotationSpeed * 50f;
            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(smoothRotation);
        }
    }
}
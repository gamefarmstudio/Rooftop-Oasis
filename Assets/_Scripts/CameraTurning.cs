using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTurning : MonoBehaviour
{
    [Header("Isometric Positioning")]
    [Tooltip("The object the camera revolves around (e.g., the Player).")]
    [SerializeField] private Transform target;
    [Tooltip("How far back the camera sits from the target.")]
    [SerializeField] private float distance = 15f;
    [Tooltip("The fixed downward angle. 30, 45, or 60 are common isometric pitches.")]
    [SerializeField] private float fixedPitch = 45f;

    [Header("Turning Settings")]
    [SerializeField] private float turnSpeed = 90f;

    [Header("Input")]
    [Tooltip("Bind a 1D Axis here. Positive = Q (Left), Negative = E (Right)")]
    [SerializeField] private InputAction turnAction;

    private float currentYRotation = 45f;

    private void OnEnable()
    {
        turnAction.Enable();
    }

    private void OnDisable()
    {
        turnAction.Disable();
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Isometric Camera needs a Target to orbit!");
            return;
        }

        float turnInput = turnAction.ReadValue<float>();

        currentYRotation += turnInput * turnSpeed * Time.deltaTime;

        Quaternion isometricRotation = Quaternion.Euler(fixedPitch, currentYRotation, 0f);

        Vector3 positionOffset = isometricRotation * (Vector3.back * distance);

        transform.position = target.position + positionOffset;
        transform.rotation = isometricRotation;
    }


}

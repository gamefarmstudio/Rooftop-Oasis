using UnityEngine;

public class CameraTurning : MonoBehaviour
{
    [Header("Camera Turning Settings")]
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] private float maxTurnAngle = 45f;
    [Header("References")]
    [SerializeField] private Transform Camera;
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            check(Camera.rotation.x, true);
            Camera.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            check(Camera.rotation.x,false);
            Camera.Rotate(Vector3.down, turnSpeed * Time.deltaTime);
        }
    }

    private void check(float direction, bool minus = false)
    {
        if (!minus && direction > maxTurnAngle)
        {
            direction = maxTurnAngle;
        }
        else if (minus && direction < -maxTurnAngle)
        {
            direction = -maxTurnAngle;
        }
    }
}

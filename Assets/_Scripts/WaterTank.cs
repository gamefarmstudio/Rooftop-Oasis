using UnityEngine;

public class WaterTank : MonoBehaviour
{
    private void Awake()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
    }
    private void FixedUpdate()
    {
        if (Weather.instance.isRaining)
        {
            ResourceManager.instance.AddWater(0.5f * Time.fixedDeltaTime);
        }
    }
}

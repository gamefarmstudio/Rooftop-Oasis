using UnityEngine;

public class PlantInformation : MonoBehaviour
{
    public float growthSpeed = 10f;
    public float PMAbsorb = 5.0f; // Set your absorption power here in the Inspector
    public bool isFinalStage = false; // PlantGrow.cs flips this switch to TRUE when it's done growing

    private void Update()
    {
        // Only start cleaning the air if PlantGrow says we are at the final stage
        if (isFinalStage && EcoSystem.instance != null)
        {
            // This is the exact math you wrote, now running safely every frame!
            float drainAmount = (PMAbsorb / 60f * Time.deltaTime);

            // Send it to the EcoSystem
            EcoSystem.instance.IncreasePMLevel(-drainAmount);
            EcoSystem.instance.UpdateUI();
        }
    }
}
using UnityEngine;

public class PlantInformation : MonoBehaviour
{
    public float growthSpeed = 10f;
    public float PMAbsorb = 5.0f; // Set your absorption power here in the Inspector
    public bool isFinalStage = false; // PlantGrow.cs flips this switch to TRUE when it's done growing
    public float timeToDie = 60f; // Time in seconds after reaching final stage before the plant dies
    public float deathTimer = 0f; // Internal timer to track time since reaching final stage

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

            // Increment the death timer
            deathTimer += Time.deltaTime;
            if (deathTimer >= timeToDie)
            {
                Debug.Log("Plant has reached the end of its life cycle and will now die.");
                gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Lay the plant down to indicate it's dead
            }
            if (deathTimer >= timeToDie + 5.5f) // Optional: Destroy the plant after 30 seconds of being "dead"
            {
                Destroy(gameObject);
            }
        }
    }
}
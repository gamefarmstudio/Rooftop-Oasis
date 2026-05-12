using UnityEngine;

public class SolarPanels : MonoBehaviour
{
    [Header("Energy Generation Settings")]
    [SerializeField] private float energyGenerationRate = 1f;
    public float energyGenerationTimer = 0f;

    private void Update()
    {
        energyGenerationTimer += Time.deltaTime;
        if (energyGenerationTimer >= 2.5f)
        {
            GenerateEnergy();
            energyGenerationTimer = 0f;
        }
    }

    private void GenerateEnergy()
    {
        if (ResourceManager.instance != null)
        {
            ResourceManager.instance.currentEnergy += 1;
            
            if (ResourceManager.instance.currentEnergy > ResourceManager.instance.maxEnergy)
            {
                ResourceManager.instance.currentEnergy = ResourceManager.instance.maxEnergy;
            }
            ResourceManager.instance.UpdateAllUI();
        }
    }
}

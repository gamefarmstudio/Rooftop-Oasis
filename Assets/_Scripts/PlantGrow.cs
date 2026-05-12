using UnityEngine;

public class PlantGrow : MonoBehaviour
{
    private PlantInformation plantInfo;
    private float growthTimer = 0f;

    [Header("NSC 2026 Relay System")]
    [SerializeField] private GameObject[] growthStages;
    [SerializeField] private int currentStageIndex = 0;
    [SerializeField] private float normalGrowthSpeed;
    [SerializeField] private float PMAbsorbing = 0f;

    private void Awake()
    {
        plantInfo = GetComponent<PlantInformation>();
        if (plantInfo != null)
        {
            normalGrowthSpeed = plantInfo.growthSpeed;
            PMAbsorbing = plantInfo.PMAbsorb;
        }
    }

    private void Update()
    {
        growthTimer += Time.deltaTime;

        if (growthTimer >= plantInfo.growthSpeed)
        {
            growthTimer = -999f;
            GrowToNextPrefab();
        }

        // Rain logic
        if (Weather.instance != null && Weather.instance.isRaining)
        {
            plantInfo.growthSpeed = normalGrowthSpeed * 0.25f;
        }
        else
        {
            plantInfo.growthSpeed = normalGrowthSpeed;
        }
    }

    private void GrowToNextPrefab()
    {
        int nextIndex = currentStageIndex + 1;

        if (nextIndex < growthStages.Length && growthStages[nextIndex] != null)
        {
            GameObject nextPlant = Instantiate(growthStages[nextIndex], transform.position, transform.rotation);

            PlantGrow nextGrow = nextPlant.GetComponent<PlantGrow>();
            PlantInformation nextInfo = nextPlant.GetComponent<PlantInformation>();

            if (nextInfo != null && plantInfo != null)
            {
                nextInfo.growthSpeed = plantInfo.growthSpeed;
                nextInfo.PMAbsorb = plantInfo.PMAbsorb;
            }

            if (nextGrow != null)
            {
                nextGrow.currentStageIndex = nextIndex;
                nextGrow.growthStages = growthStages;
            }

            Destroy(this.gameObject);
        }
        else
        {
            // --- THIS IS THE FINAL STAGE ---
            if (plantInfo != null)
            {
                // 1. Flip the switch!
                plantInfo.isFinalStage = true;
                Debug.Log("Final Stage Active: Commencing PM 2.5 Absorption.");
            }

            // 2. Remove the growth script so it stops wasting CPU, but keep the plant
            Destroy(this);
        }
    }
}
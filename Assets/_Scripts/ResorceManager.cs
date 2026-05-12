using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    [Header("Coins (Simple Integer)")]
    public int coins = 1000;

    [Header("Energy Settings")]
    public float currentEnergy = 100f;
    public float maxEnergy = 100f;

    [Header("Water Settings")]
    public float currentWater = 50f;
    public float maxWater = 100f;

    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI energyText;
    [SerializeField] private TextMeshProUGUI waterText;

    [Header("UI Slider References")]
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider waterSlider;

    

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateAllUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateAllUI();
            return true;
        }
        return false;
    }

    public void UpdateAllUI()
    {
        float energyRatio = currentEnergy / maxEnergy;
        if (energyText != null)
            energyText.text = (energyRatio * 100f).ToString("F0") + "%";

        if (energySlider != null)
            energySlider.value = energyRatio;

        float waterRatio = currentWater / maxWater;
        if (waterText != null)
            waterText.text = (waterRatio * 100f).ToString("F0") + "%";


        if (waterSlider != null)
            waterSlider.value = waterRatio;
        coinText.text = coins.ToString();
        
        
    }

    public void AddEnergy(float amount)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);
        UpdateAllUI();
    }
    private void FixedUpdate()
    {
        UpdateAllUI();
    }
    public void AddWater(float amount)
    {
        currentWater = Mathf.Clamp(currentWater + amount, 0, maxWater);
        UpdateAllUI();
    }
}
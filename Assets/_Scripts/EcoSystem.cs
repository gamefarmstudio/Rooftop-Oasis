using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EcoSystem : MonoBehaviour
{
    public static EcoSystem instance;
    public int EcoPoints { get; set; } = 50;
    public float PMLevel { get; private set; } = 250.0f;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI ecoSystemUI;
    [SerializeField] private TextMeshProUGUI pmLevelUI;
    [SerializeField] private Slider pmLevelSlider;

    [Header("Effects")]
    [SerializeField] private ParticleSystem pollutionEffect;

    private Image sliderFillImage;
    private float uiRefreshTimer = 0f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        Debug.Log($"EcoSystem instance created({this.gameObject.name}).");
    }

    private void Start()
    {
        if (pmLevelSlider != null && pmLevelSlider.fillRect != null)
        {
            sliderFillImage = pmLevelSlider.fillRect.GetComponent<Image>();
        }

        // Run UI update once at the start to set everything up
        UpdateUI();
    }

    private void Update()
    {
        // --- THE UI THROTTLE ---
        // Instead of updating the heavy UI and Particle System 600 times a second, 
        // this limits it to exactly 10 times a second (every 0.1 seconds).
        uiRefreshTimer += Time.deltaTime;

        if (uiRefreshTimer >= 0.1f)
        {
            UpdateUI();
            uiRefreshTimer = 0f;
        }
    }

    public void AddEcoPoints(int points)
    {
        EcoPoints += points;
        UpdateUI(); // It is okay to instantly update when points are added
    }

    public void IncreasePMLevel(float amount)
    {
        // --- THE MATH ENGINE ---
        // This calculates the math as fast as the plants send it, 
        // but it NO LONGER triggers the heavy UpdateUI() function!
        PMLevel = Mathf.Clamp(PMLevel + amount, 0, 250);
    }

    public void UpdateUI()
    {
        // 1. Update Text
        if (ecoSystemUI != null) ecoSystemUI.text = EcoPoints.ToString();
        if (pmLevelUI != null) pmLevelUI.text = $"PM Level: {PMLevel:F1}";

        // 2. Update Slider Graphic and Color
        if (pmLevelSlider != null)
        {
            float normalizedLevel = PMLevel / 250f;
            pmLevelSlider.value = normalizedLevel;

            if (sliderFillImage != null)
            {
                sliderFillImage.color = Color.Lerp(Color.green, Color.red, normalizedLevel);
            }
        }

        // 3. Update the Modern Particle System
        if (pollutionEffect != null)
        {
            var emission = pollutionEffect.emission;
            emission.rateOverTime = PMLevel;

            var main = pollutionEffect.main;
            float newLifetime = Mathf.Max(0.1f, PMLevel / 50f);
            main.startLifetime = newLifetime;
        }
    }
}
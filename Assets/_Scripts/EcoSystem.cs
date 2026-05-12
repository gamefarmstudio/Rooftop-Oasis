using UnityEngine;
using TMPro;
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

    private Image sliderFillImage;

    private void Start()
    {
        if (pmLevelSlider != null && pmLevelSlider.fillRect != null)
        {
            sliderFillImage = pmLevelSlider.fillRect.GetComponent<Image>();
        }

        UpdateUI();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this; // "I am the active EcoSystem!"
        Debug.Log($"EcoSystem instance created({this.gameObject.name}).");
    }

    public void AddEcoPoints(int points)
    {
        EcoPoints += points;
        UpdateUI();
    }

    public void IncreasePMLevel(float amount)
    {
        PMLevel = Mathf.Clamp(PMLevel + amount, 0, 250);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (ecoSystemUI != null) ecoSystemUI.text =  EcoPoints.ToString();
        if (pmLevelUI != null) pmLevelUI.text = $"PM Level: {PMLevel:F1}%";

        if (pmLevelSlider != null)
        {
            float normalizedLevel = PMLevel / 250f;
            pmLevelSlider.value = normalizedLevel;

            if (sliderFillImage != null)
            {
                sliderFillImage.color = Color.Lerp(Color.green, Color.red, normalizedLevel);
            }
        }
    }
}

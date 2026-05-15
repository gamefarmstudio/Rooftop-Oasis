using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    [Tooltip("How many real-world seconds a full in-game day takes.")]
    public float dayDurationInSeconds = 120f;

    [Range(0f, 1f)]
    [Tooltip("0 = Sunrise, 0.25 = Noon, 0.5 = Sunset, 0.75 = Midnight")]
    public float timeOfDay = 0.25f; // Starts at Noon

    [Header("Sun Settings")]
    public Light sunLight;

    [Header("City Lights")]
    [Tooltip("Drag the GameObjects holding your street lights and traffic lights here.")]
    public Light[] cityLightComponents; // Optional: If you want to directly control the Light components

    // We store the starting intensity so the sun doesn't get too bright
    private float defaultSunIntensity;

    // Track if lights are on to prevent toggling them every single frame
    private bool areLightsOn = true;

    private void Start()
    {
        if (sunLight != null)
        {
            defaultSunIntensity = sunLight.intensity;
        }
        else
        {
            Debug.LogWarning("DayNightCycle: Please assign the Directional Light to the Sun Light slot!");
        }

        // Initialize lights based on starting time
        CheckCityLights();
    }
    private void Awake()
    {
        CheckCityLights();
    }

    private void Update()
    {
        if (sunLight == null) return;
        CheckCityLights();
        // 1. Advance the time 
        timeOfDay += Time.deltaTime / dayDurationInSeconds;

        // 2. Loop it back to 0 (Sunrise) when it hits 1 (End of the night)
        if (timeOfDay >= 1f)
        {
            timeOfDay = 0f;
        }

        // 3. Update the Sun's rotation
        UpdateSun();

        // 4. Update the Streetlights
        
    }

    private void UpdateSun()
    {
        // A full circle is 360 degrees. 
        // 0 = Sunrise (0 deg), 0.25 = Noon (90 deg), 0.5 = Sunset (180 deg), 0.75 = Midnight (270 deg)
        float sunAngle = timeOfDay * 360f;

        // Rotate the sun. The 170f just gives the shadows a nice angle!
        sunLight.transform.localRotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // A sine wave is the perfect curve for the sun! 
        // It naturally rises to 1 at noon (0.25) and falls to 0 at sunset (0.5)
        float intensityMultiplier = Mathf.Clamp01(Mathf.Sin(timeOfDay * Mathf.PI * 2f));

        // Fade the sun physically
        sunLight.intensity = defaultSunIntensity * intensityMultiplier;

        // CRITICAL FIX: Dim the ambient environment light!
        // Without this, the skybox and ambient light will keep your city bright even without the sun.
        // We clamp it between 0.05f and 1f so the city is dark, but not pitch black.
        RenderSettings.ambientIntensity = Mathf.Clamp(intensityMultiplier, 0.05f, 1f) + 0.5f;
    }

    private void CheckCityLights()
    {
        // Define nighttime as just before sunset (0.48) to just after sunrise (0.98)
        bool isNightTime = timeOfDay >= 0.48f && timeOfDay <= 0.98f;

        // Turn lights ON if it's night and they are currently off
        if (isNightTime && !areLightsOn)
        {
            ToggleCityLights(true);
        }
        // Turn lights OFF if it's day and they are currently on
        else if (!isNightTime && areLightsOn)
        {
            ToggleCityLights(false);
        }
    }

    private void ToggleCityLights(bool turnOn)
    {
        areLightsOn = turnOn;

        foreach (Light lightComponent in cityLightComponents)
        {
            
            if (lightComponent != null)
            {
                lightComponent.enabled = turnOn;
            }
        }

        Debug.Log("City lights turned " + (turnOn ? "ON" : "OFF"));
    }
}
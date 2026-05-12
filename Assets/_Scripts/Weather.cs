using UnityEngine;

public class Weather : MonoBehaviour
{
    public static Weather instance;

    [Header("Rain Settings")]
    [SerializeField] private ParticleSystem rainParticleSystem;
    public bool isRaining = false;

    [Header("Timers (Visible for Debugging)")]
    [SerializeField] private float timeUntilNextRain = 0f;
    [SerializeField] private float currentRainDuration = 0f;
    private float timer = 0f;

    private void Awake()
    {
        if (instance == null) instance = this;

        timeUntilNextRain = Random.Range(50f, 200f);
        timer = 0f;

        if (rainParticleSystem != null)
            rainParticleSystem.Stop();
    }

    private void Update()
    {
        if (!isRaining)
        {
            timer += Time.deltaTime;

            if (timer >= timeUntilNextRain)
            {
                StartRain();
                currentRainDuration = Random.Range(60f, 150f);
                timer = 0f;
            }
        }
        else
        {
            timer += Time.deltaTime;


            // Check if it's time to stop raining
            if (timer >= currentRainDuration)
            {
                StopRain();
                timeUntilNextRain = Random.Range(50f, 200f);
                timer = 0f; // Reset timer to count the waiting period
            }
        }
    }

    public void StartRain()
    {
        if (!isRaining && rainParticleSystem != null)
        {
            rainParticleSystem.Play();
            isRaining = true;
            Debug.Log("🌦️ Weather: It started raining!");
        }
    }

    public void StopRain()
    {
        if (isRaining && rainParticleSystem != null)
        {
            rainParticleSystem.Stop();
            isRaining = false;
            Debug.Log("☀️ Weather: The rain stopped.");
        }
    }
}
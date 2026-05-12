using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { MainMenu, Exploration, Shop, Paused }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game State")]
    public GameState currentState;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateState(GameState.Exploration);
    }

    public void UpdateState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Exploration:
                HandleExploration();
                break;
            case GameState.Shop:
                HandleShop();
                break;
            case GameState.Paused:
                HandlePaused();
                break;
        }

        Debug.Log("Game State Changed to: " + newState);
    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void HandleMainMenu() { /* Setup Menu UI */ }

    private void HandleExploration()
    {
        Time.timeScale = 1f;
       
    }

    private void HandleShop()
    {
        
    }

    private void HandlePaused()
    {
        Time.timeScale = 0f;
    }
}
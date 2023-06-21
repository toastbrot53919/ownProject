using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { InMenu, Playing, Paused, GameOver }
    public GameState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize game state and other systems as needed
        currentState = GameState.InMenu;
    }

    private void Update()
    {
        HandleGameState();
        UpdateCursorVisibility();
    }

    private void UpdateCursorVisibility()
    {
        // If the game is paused or in a menu, show the cursor
        if (currentState == GameState.Paused || currentState == GameState.InMenu)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // If the game is playing, hide the cursor
        else if (currentState == GameState.Playing)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void HandleGameState()
    {
        switch (currentState)
        {
            case GameState.InMenu:
                // Handle main menu logic
                break;
            case GameState.Playing:
                // Handle playing state logic
                break;
            case GameState.Paused:
                // Handle paused state logic
                break;
            case GameState.GameOver:
                // Handle game over logic
                break;
        }
    }

    public void ChangeGameState(GameState newState)
    {
        currentState = newState;
    }
    public void SaveGame()
    {
       GameObject player = GameObject.FindGameObjectWithTag("Player");
       SerializeManager.SavePlayer(player);
    }

    public void LoadGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        SerializeManager.LoadPlayer(player);
    }
    // Implement other methods as needed, such as SaveGame, LoadGame, etc.
}
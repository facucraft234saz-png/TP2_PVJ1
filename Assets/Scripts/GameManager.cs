
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Playing,
        Paused,
        Victory,
        GameOver
    }

    public static GameManager Instance { get; private set; }

    [Header("Estado")]
    public GameState CurrentState { get; private set; }

    [Header("Menus")]
    public GameObject pausePanel;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    [Header("Referencias")]
    public PlayerHealth playerHealth;
    public ScoreManager scoreManager;

    [Header("Registro por consola")]
    public float logInterval = 1f;

    private float logTimer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        ChangeState(GameState.Start);
        ChangeState(GameState.Playing);
    }

    void Update()
    {
        // ESC solo alterna pausa durante una partida activa.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (CurrentState == GameState.Paused)
            {
                ResumeGame();
            }
        }

        // Registrar vida y puntaje usando tiempo escalado.
        if (CurrentState == GameState.Playing)
        {
            logTimer += Time.deltaTime;

            if (logTimer >= logInterval)
            {
                logTimer = 0f;
                LogPlayerStatus();
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        Debug.Log("Estado del juego: " + CurrentState);

        Time.timeScale =
            newState == GameState.Paused ||
            newState == GameState.Victory ||
            newState == GameState.GameOver
            ? 0f
            : 1f;

        if (pausePanel != null)
            pausePanel.SetActive(newState == GameState.Paused);

        if (victoryPanel != null)
            victoryPanel.SetActive(newState == GameState.Victory);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(newState == GameState.GameOver);

        if (newState == GameState.Victory ||
            newState == GameState.GameOver)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        ChangeState(GameState.Paused);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;

        ChangeState(GameState.Playing);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void WinGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        LogPlayerStatus();
        ChangeState(GameState.Victory);

        Debug.Log("¡Victoria! La misión fue completada.");
    }

    public void LoseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        LogPlayerStatus();
        ChangeState(GameState.GameOver);

        Debug.Log("Game Over: el jugador perdió toda su vida.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        logTimer = 0f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void LogPlayerStatus()
    {
        string healthInfo = playerHealth != null
            ? Mathf.CeilToInt(playerHealth.currentHealth) +
              "/" + Mathf.CeilToInt(playerHealth.maxHealth)
            : "Sin referencia";

        string scoreInfo = scoreManager != null
            ? scoreManager.Score.ToString()
            : "Sin referencia";

        Debug.Log(
            "Estado: " + CurrentState +
            " | Vida: " + healthInfo +
            " | Puntaje: " + scoreInfo
        );
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
            Time.timeScale = 1f;
        }
    }
}

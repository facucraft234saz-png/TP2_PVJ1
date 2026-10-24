
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Puntaje")]
    public int Score { get; private set; }

    [Header("Interfaz")]
    public TMP_Text scoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        UpdateScoreUI();
    }

    public void AddPoints(int amount)
    {
        if (amount <= 0)
            return;

        Score += amount;

        UpdateScoreUI();

        Debug.Log("Puntaje actual: " + Score);
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntaje: " + Score;
        }
    }
}

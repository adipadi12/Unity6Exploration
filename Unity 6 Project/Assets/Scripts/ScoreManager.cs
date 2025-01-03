using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to the UI Text

    private int score = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }
}

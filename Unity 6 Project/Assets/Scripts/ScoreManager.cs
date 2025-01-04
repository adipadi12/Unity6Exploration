using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance
    public TextMeshProUGUI scoreText; // Reference to the UI Text

    [SerializeField] private AudioClip destroySound; // Assign this in the Inspector
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(int points)
    {
        score += points;
        audioSource.PlayOneShot(destroySound);
        scoreText.text = score.ToString();
    }
}

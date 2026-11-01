
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Gameplay UI")]
    public GameObject gameUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;

    [Header("Win and Game Over")]
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI winFinalScoreText;
    public TextMeshProUGUI gameOverFinalScoreText;

    [Header("Leaderboard")]
    public GameObject leaderboardPanel;

    [Header("Gameplay Objects")]
    public GameObject gameplayObjects;

    [Header("Sounds")]
    public AudioSource winSound;
    public AudioSource gameOverSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Show gameplay UI
        if (gameUI != null)
            gameUI.SetActive(true);

        // Hide end screens
        if (winPanel != null)
            winPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Hide leaderboard at the beginning
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(false);

        // Show gameplay objects
        if (gameplayObjects != null)
            gameplayObjects.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
            scoreText.text = score + "/30";
    }

    public void UpdateLives(int lives)
    {
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    public void UpdateLevel(int level)
    {
        if (levelText != null)
            levelText.text = "Level: " + level;
    }

    public void ShowGameOver()
    {
        if (gameOverSound != null)
            gameOverSound.Play();

        ShowEndScreen(false);
    }

    public void ShowWin()
    {
        if (winSound != null)
            winSound.Play();

        ShowEndScreen(true);
    }

    private void ShowEndScreen(bool won)
    {
        // Hide gameplay UI
        if (gameUI != null)
            gameUI.SetActive(false);

        // Stop gameplay objects
        if (gameplayObjects != null)
            gameplayObjects.SetActive(false);

        // Stop spawning
        StarSpawner spawner =
            FindFirstObjectByType<StarSpawner>();

        if (spawner != null)
            spawner.StopSpawning();

        // Show leaderboard panel
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(true);

        // Show correct end screen
        if (winPanel != null)
            winPanel.SetActive(won);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(!won);

        // Display final score
        int finalScore = ScoreManager.Instance.score;

        if (won && winFinalScoreText != null)
        {
            winFinalScoreText.text =
                "Your Score: " + finalScore + "/30";
        }

        if (!won && gameOverFinalScoreText != null)
        {
            gameOverFinalScoreText.text =
                "Your Score: " + finalScore + "/30";
        }
    }
}

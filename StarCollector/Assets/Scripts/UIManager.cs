
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
        gameUI.SetActive(true);
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        leaderboardPanel.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score + "/30";
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }

    public void UpdateLevel(int level)
    {
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
        gameUI.SetActive(false);

        if (gameplayObjects != null)
            gameplayObjects.SetActive(false);

        StarSpawner spawner =
            FindFirstObjectByType<StarSpawner>();

        if (spawner != null)
            spawner.StopSpawning();

        leaderboardPanel.SetActive(true);

        winPanel.SetActive(won);
        gameOverPanel.SetActive(!won);

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

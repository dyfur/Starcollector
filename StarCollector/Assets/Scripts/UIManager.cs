using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI levelText;

    public GameObject gameOverPanel;
    public GameObject winPanel;

    public AudioSource winSound;
    public AudioSource gameOverSound;

    private void Awake()
    {
        Instance = this;
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
        {
            gameOverSound.Play();
        }

        gameOverPanel.SetActive(true);
    }

    public void ShowWin()
    {
        if (winSound != null)
        {
            winSound.Play();
        }

        winPanel.SetActive(true);
    }
}
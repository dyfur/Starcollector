using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public static LivesManager Instance;

    public int lives = 3;

    private bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.UpdateLives(lives);
    }

    public void LoseLife()
    {
        if (gameOver)
            return;

        lives--;

        UIManager.Instance.UpdateLives(lives);

        if (lives <= 0)
        {
            gameOver = true;

            UIManager.Instance.ShowGameOver();

            Invoke(nameof(PauseGame), 1f);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    private int level = 1;

    private bool gameWon = false;

    private StarSpawner starSpawner;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        starSpawner = FindFirstObjectByType<StarSpawner>();

        UIManager.Instance.UpdateScore(score);
        UIManager.Instance.UpdateLevel(level);
    }

    public void AddScore()
    {
        if (gameWon)
            return;

        score++;

        UIManager.Instance.UpdateScore(score);

        if (score == 10)
        {
            level = 2;
            
            UIManager.Instance.UpdateLevel(level);
            
            starSpawner.SetLevel2();
        }

        if (score == 20)
        {
            level = 3;
            
            UIManager.Instance.UpdateLevel(level);
            starSpawner.SetLevel3();
        }
        if (score >= 30)
        {
            gameWon = true;

            UIManager.Instance.ShowWin();

             Invoke(nameof(PauseGame), 1f);

        }
        
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
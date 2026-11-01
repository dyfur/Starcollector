
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Reset game time
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}

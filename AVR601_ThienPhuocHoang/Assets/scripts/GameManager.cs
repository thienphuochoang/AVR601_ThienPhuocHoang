using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score.ToString("00000");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        AudioManager.Instance.PlayGameOver();
        StartCoroutine(FreezeAfterSound());
    }

    System.Collections.IEnumerator FreezeAfterSound()
    {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
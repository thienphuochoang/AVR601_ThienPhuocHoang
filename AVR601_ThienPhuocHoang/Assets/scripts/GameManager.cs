using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public GameObject gameOverScreen;

    void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        AudioManager.Instance.PlayGameOver();
        StartCoroutine(FreezeAfterSound());
    }

    IEnumerator FreezeAfterSound()
    {
        yield return new WaitForSecondsRealtime(1f); // wait 1 sec for sound to play
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
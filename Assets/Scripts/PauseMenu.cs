using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    private bool GameIsPaused = false;
    public GameManager GameManager;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CallMenu();
    }

    public void CallMenu()
    {
        if (GameManager.isOnGame)
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        SFXManager.Instance?.PlayClick();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        SFXManager.Instance?.PlayClick();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMainMenu()
    {
        StartCoroutine(PlayClickThenLoad("MainMenu"));
    }

    public void LoadCredits()
    {
        StartCoroutine(PlayClickThenLoad("Credits"));
    }

    private IEnumerator PlayClickThenLoad(string sceneName)
    {
        SFXManager.Instance?.PlayClick();
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
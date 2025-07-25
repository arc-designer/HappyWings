using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    private int score;
    public bool isOnGame = false;
    private Pipes[] pipes;

    public Parallax backgroundParallax;
    public Parallax groundParallax;

    [Header("Player")]
    public GameObject Player;

    [Header("Managers")]
    public GameObject SpawnManager;

    [Header("Scripts")]
    public MainMenu MainMenu;

    [Header("Overlay")]
    public GameObject scoreObject;
    private TextMeshProUGUI scoreText;
    public GameObject overlayGameOver;

    [Header("Buttons")]
    public GameObject playButton;

    void Awake()
    {
        scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        isOnGame = true;

        if (MainMenu.isOnMainMenu)
        {
            MainMenu.isOnMainMenu = false;
            ReplayGame();
        }

        Application.targetFrameRate = 120;
        Player.SetActive(false);
        ResetScore();
        ReplayGame();
    }

    void IncreaseDifficulty()
    {
        Debug.LogWarning("Increase difficulty");
    }

    public void ReplayGame()
    {
        ResetScore();
        playButton.SetActive(false);
        overlayGameOver.SetActive(false);
        scoreObject.SetActive(true);
        Player.SetActive(true);
        Player.transform.position = new Vector3(0f,0f,1f);

        Time.timeScale = 1f;
        isOnGame = true;

        pipes = FindObjectsOfType<Pipes>();
        for (int i=0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        overlayGameOver.SetActive(true);
        playButton.SetActive(true);
        isOnGame = false;
        WaitingRestart();
    }

    public void WaitingRestart()
    {
        Time.timeScale = 0f;
        isOnGame = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    
    public void LoadCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }
}
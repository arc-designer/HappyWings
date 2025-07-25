using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer spriteRenderer;
    public bool isOnMainMenu = false;
    public Sprite[] spriteArr;
    private int spriteIndex;

    void Awake()
    {
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        isOnMainMenu = true;
        Time.timeScale = 1f;
    }

    public void Start()
    {
        if (spriteArr != null && spriteArr.Length > 0)
        {
            InvokeRepeating(nameof(FlappingWingsBird), 0.15f, 0.15f);
        }
        else
        {
            Debug.LogWarning("Sprite array is empty or null.");
        }
    }

    void FlappingWingsBird()
    {
        if (isOnMainMenu && spriteArr.Length > 0)
        {
            spriteIndex++;
            if (spriteIndex >= spriteArr.Length)
                spriteIndex = 0;

            spriteRenderer.sprite = spriteArr[spriteIndex];
        }
    }

    public void PlayGame()
    {
        SFXManager.Instance?.PlayClick(); // ✅ Play click sound
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        SFXManager.Instance?.PlayClick(); // ✅ Play click sound
        Debug.LogWarning("Closing the game... (not visible in the editor)");
        Application.Quit();
    }
}
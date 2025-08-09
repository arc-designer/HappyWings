using UnityEngine;

public class Player : MonoBehaviour
{
    public MainMenu mainMenu;

    private Vector3 direction;
    public float gravity = -9.8f;
    public int strength = 5;

    private SpriteRenderer spriteRenderer;
    public Sprite[] spriteArr;
    private int spriteIndex;

    // 🔼 Double Tap fields
    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;
    public float doubleTapBoostStrength = 10f;

    // 🔽 Swipe Down fields
    private Vector2 touchStartPos;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            direction = Vector3.up * strength;
        }

        // 📱 Mobile Touch Controls
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Store start pos for swipe
                touchStartPos = touch.position;

                // Check for double tap
                float currentTime = Time.time;
                if (currentTime - lastTapTime < doubleTapThreshold)
                {
                    direction = Vector3.up * doubleTapBoostStrength; // 💥 Double Tap Boost
                }
                else
                {
                    direction = Vector3.up * strength;
                }
                lastTapTime = currentTime;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 touchEndPos = touch.position;
                float swipeDistanceY = touchStartPos.y - touchEndPos.y;

                if (swipeDistanceY > 100f)
                {
                    direction = Vector3.down * strength * 1.5f; 
                }
            }
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    public void Start()
    {
        InvokeRepeating(nameof(changeRenderSprite), 0.15f, 0.15f);
    }

    public void changeRenderSprite()
    {
        spriteIndex++;

        if (spriteIndex >= spriteArr.Length)
        {
            spriteIndex = 0;
        }
        if (direction.y > 0)
        {
            spriteRenderer.sprite = spriteArr[spriteIndex];
        }
        else
        {
            spriteRenderer.sprite = spriteArr[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pipe" || other.gameObject.tag == "Obstacle")
        {
            SFXManager.Instance?.PlayHit();
            FindObjectOfType<GameManager>().GameOver();
            Debug.Log("Call GameOver();");
        }
        else if (other.gameObject.tag == "ScoreZone")
        {
            SFXManager.Instance?.PlayPoint();
            FindObjectOfType<GameManager>().IncreaseScore();
            Debug.Log("Call IncreaseScore();");
        }
    }
}
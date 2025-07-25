using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource hitSFX;
    public AudioSource pointSFX;
    public AudioSource clickSFX;

    public static SFXManager Instance;

    void Awake()
    {
        // ✅ Persist across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 🔁 Persist when switching scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // ✅ Prevent duplicates when switching back
        }
    }

    public void PlayHit()
    {
        if (hitSFX != null && !hitSFX.isPlaying)
            hitSFX.Play();
    }

    public void PlayPoint()
    {
        if (pointSFX != null && !pointSFX.isPlaying)
            pointSFX.Play();
    }

    public void PlayClick()
    {
        if (clickSFX != null && !clickSFX.isPlaying)
            clickSFX.Play();
    }
}
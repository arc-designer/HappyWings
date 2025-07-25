using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Vector2 startPos;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
using UnityEngine;

public class InfiniteBackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    private float backgroundWidth;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        backgroundWidth = spriteRenderer.bounds.size.x;
    }

    void Update()
    {
        // Move the background horizontally based on time and speed
        float offsetX = Time.time * scrollSpeed;

        // Calculate the actual position of the background
        float newPosition = offsetX % backgroundWidth;

        // Apply the new position to the background
        transform.position = new Vector3(newPosition, transform.position.y, transform.position.z);
    }
}

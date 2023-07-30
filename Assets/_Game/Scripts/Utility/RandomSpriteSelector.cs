using UnityEngine;

public class RandomSpriteSelector : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found! Attach the script to a GameObject with a SpriteRenderer.");
        }
    }

    private void Start()
    {
        if (sprites.Length > 0)
        {
            // Select a random sprite from the array
            int randomIndex = Random.Range(0, sprites.Length);
            Sprite randomSprite = sprites[randomIndex];

            // Assign the random sprite to the SpriteRenderer component
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = randomSprite;
            }
        }
        else
        {
            Debug.LogWarning("The sprites array is empty. Please assign sprites to the array in the Inspector.");
        }
    }
}

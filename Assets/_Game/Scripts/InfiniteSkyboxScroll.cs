using UnityEngine;

public class InfiniteSkyboxScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private Transform cloneTransform;
    private GameObject cloneObject;
    private SpriteRenderer cloneSpriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.size.x;

        // Create a clone sprite and position it to the right of the original sprite
        cloneObject = new GameObject("CloneSprite");

        cloneSpriteRenderer = cloneObject.AddComponent<SpriteRenderer>();
        cloneSpriteRenderer.sprite = spriteRenderer.sprite;
        cloneSpriteRenderer.flipX = true; // Ensure the clone is flipped horizontally

        // Copy essential properties from the original sprite renderer to the clone
        cloneSpriteRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
        cloneSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
        cloneSpriteRenderer.color = spriteRenderer.color;
        cloneSpriteRenderer.sharedMaterial = spriteRenderer.sharedMaterial;

        // Additional properties to copy
        cloneSpriteRenderer.drawMode = spriteRenderer.drawMode;
        cloneSpriteRenderer.size = spriteRenderer.size;
        cloneSpriteRenderer.maskInteraction = spriteRenderer.maskInteraction;
        cloneSpriteRenderer.spriteSortPoint = spriteRenderer.spriteSortPoint;

        cloneTransform = cloneObject.transform;
        cloneTransform.position = transform.position + new Vector3(spriteWidth, 0f, 0f);
    }

    void Update()
    {
        // Move the original and clone sprites horizontally based on time and speed
        float offsetX = Time.deltaTime * scrollSpeed;
        transform.position -= new Vector3(offsetX, 0f, 0f);
        cloneTransform.position -= new Vector3(offsetX, 0f, 0f);

        // Reset the positions of the original and clone sprites when they go off-screen
        if (transform.position.x <= -spriteWidth)
        {
            transform.position += new Vector3(spriteWidth * 2, 0f, 0f);
        }
        if (cloneTransform.position.x <= -spriteWidth)
        {
            cloneTransform.position += new Vector3(spriteWidth * 2, 0f, 0f);
        }
    }
}

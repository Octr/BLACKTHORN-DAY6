using UnityEngine;

public class InfiniteSkyboxScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    private Material material;
    private float offset;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    void Update()
    {
        // Move the skybox horizontally based on time and speed
        offset += Time.deltaTime * scrollSpeed;

        // Ensure the offset stays between 0 and 1
        if (offset > 1.0f)
        {
            offset -= 1.0f;
        }

        // Apply the offset to the material's main texture
        material.mainTextureOffset = new Vector2(offset, 0);
    }
}

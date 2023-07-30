using UnityEngine;

public class RainbowParticleEffect : MonoBehaviour
{
    public Renderer particleRenderer;
    public float speed = 1.0f;
    public float transitionSpeed = 1.0f;

    private Material material;
    private float hueShift;

    private void Awake()
    {
        if (particleRenderer == null)
        {
            Debug.LogWarning("Renderer not assigned. Please assign a Renderer to this script.");
            enabled = false;
            return;
        }

        material = particleRenderer.material;
        hueShift = 0.0f;
    }

    private void Update()
    {
        hueShift += speed * Time.deltaTime;
        if (hueShift > 1.0f)
        {
            hueShift -= 1.0f;
        }

        Color targetColor = Color.HSVToRGB(hueShift, 1.0f, 1.0f);

        // Apply the hue-shifted color to the material
        material.SetColor("_BaseColor", targetColor);
    }
}

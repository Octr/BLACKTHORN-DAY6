using UnityEngine;

public class SpaceshipAnimatorController : MonoBehaviour
{
    public Animator spaceshipAnimator;
    public float speedMultiplier = 0.1f;
    public float blendIncreaseSpeed = 2f; // Multiplier for increasing the blend weight
    public float blendDecreaseSpeed = 1f; // Multiplier for decreasing the blend weight
    public float blendThreshold = 0.05f; // Threshold to prevent choppiness when blend weight is close to 0
    public string blendTreeParameter = "Speed"; // The parameter name for the blend tree in the Animator

    private Vector3 previousPosition;
    private float currentSpeed = 0f;
    private float targetBlendWeight = 0f;

    private void Start()
    {
        previousPosition = transform.position;

        if (spaceshipAnimator == null)
        {
            Debug.LogError("SpaceshipAnimatorController: Missing reference to the Animator!");
            enabled = false;
        }
    }

    private void Update()
    {
        // Calculate the spaceship's speed based on the difference between current and previous positions
        float speed = (transform.position - previousPosition).magnitude / Time.deltaTime;

        // Update the previous position for the next frame
        previousPosition = transform.position;

        // Smoothly adjust the current speed based on the spaceship's speed and speedMultiplier
        currentSpeed = Mathf.Lerp(currentSpeed, speed, Time.deltaTime * blendIncreaseSpeed);

        // Calculate the target blend tree weight based on the current speed and speedMultiplier
        targetBlendWeight = currentSpeed * speedMultiplier;

        // Clamp the target blend tree weight between 0 and 1
        targetBlendWeight = Mathf.Clamp01(targetBlendWeight);

        // Smoothly adjust the blend tree weight of the Animator
        float currentBlendWeight = spaceshipAnimator.GetFloat(blendTreeParameter);

        // Check if the target blend weight is below the threshold
        if (targetBlendWeight <= blendThreshold)
        {
            spaceshipAnimator.SetFloat(blendTreeParameter, 0f);
        }
        else
        {
            spaceshipAnimator.SetFloat(blendTreeParameter, Mathf.Lerp(currentBlendWeight, targetBlendWeight, Time.deltaTime * blendDecreaseSpeed));
        }
    }
}

using UnityEngine;

public class SpaceshipAudio : MonoBehaviour
{
    public AudioSource spaceshipAudioSource;
    public float pitchMultiplier = 0.1f;
    public float volumeMultiplier = 0.1f;
    public float minPitch = 0.8f; // Adjust this to set the minimum pitch value
    public float maxPitch = 1.2f; // Adjust this to set the maximum pitch value

    private Vector3 previousPosition;

    private void Start()
    {
        previousPosition = transform.position;

        if (spaceshipAudioSource == null)
        {
            Debug.LogError("SpaceshipAudio: Missing reference to the Audio Source!");
            enabled = false;
        }
    }

    private void Update()
    {
        // Calculate the spaceship's speed based on the difference between current and previous positions
        float speed = (transform.position - previousPosition).magnitude / Time.deltaTime;

        // Update the previous position for the next frame
        previousPosition = transform.position;

        // Calculate the target pitch based on the spaceship's speed and pitchMultiplier
        float targetPitch = 1f + speed * pitchMultiplier;

        // Clamp the target pitch between the minPitch and maxPitch values
        targetPitch = Mathf.Clamp(targetPitch, minPitch, maxPitch);

        // Calculate the target volume based on the spaceship's speed and volumeMultiplier
        float targetVolume = Mathf.Clamp01(speed * volumeMultiplier);

        // Smoothly adjust the pitch and volume of the Audio Source
        spaceshipAudioSource.pitch = Mathf.Lerp(spaceshipAudioSource.pitch, targetPitch, Time.deltaTime * 10f);
        spaceshipAudioSource.volume = Mathf.Lerp(spaceshipAudioSource.volume, targetVolume, Time.deltaTime * 10f);
    }
}

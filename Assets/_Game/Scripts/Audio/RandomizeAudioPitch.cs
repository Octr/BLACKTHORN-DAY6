using UnityEngine;

public class RandomizeAudioPitch : MonoBehaviour
{
    public AudioSource audioSource;
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
    public bool keepChecking = true;

    private void Start()
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource not assigned. Cannot play audio.");
            return;
        }

        RandomizeAndPlay();
    }

    private void Update()
    {
        if (keepChecking && audioSource != null && !audioSource.isPlaying)
        {
            RandomizeAndPlay();
        }
    }

    private void RandomizeAndPlay()
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        // Round the random pitch to the nearest decimal place
        randomPitch = Mathf.Round(randomPitch * 10f) / 10f;
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }
}

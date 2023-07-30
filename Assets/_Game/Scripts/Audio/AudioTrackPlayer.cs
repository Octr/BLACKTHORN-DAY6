using UnityEngine;
using TMPro;

public class AudioTrackPlayer : MonoBehaviour
{
    [System.Serializable]
    public class AudioTrack
    {
        public AudioClip audioClip;
    }

    public AudioTrack[] audioTracks;
    public TextMeshProUGUI subtitleText;
    public float delayBetweenTracks = 1.0f;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;

    public string[] subtitles; // Array to store subtitles

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        PlayNextTrack();
    }

    private void PlayNextTrack()
    {
        if (currentTrackIndex >= audioTracks.Length)
        {
            // All tracks played, call the method after a delay
            Invoke("AllTracksFinished", delayBetweenTracks);
            return;
        }

        // Play the audio clip from the current track
        audioSource.clip = audioTracks[currentTrackIndex].audioClip;
        audioSource.Play();

        // Update the subtitle text for the current audio track
        UpdateSubtitleText(subtitles[currentTrackIndex]);

        // Move to the next track index
        currentTrackIndex++;

        // Call PlayNextTrack with a delay after the current audio clip has finished playing
        Invoke("PlayNextTrack", audioSource.clip.length + delayBetweenTracks);
    }

    private void UpdateSubtitleText(string subtitle)
    {
        subtitleText.text = subtitle;
    }

    private void AllTracksFinished()
    {
        // All audio tracks are finished playing
        // Call your desired method here
        PlanetManager.Instance.EnablePlanets();
    }
}
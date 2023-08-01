using UnityEngine;
using System.Collections.Generic;

// AudioManager using Singleton pattern and Object Pooling for AudioSources
public class AudioManager : Singleton<AudioManager>
{
    // Object pool for audio sources
    private Queue<AudioSource> audioSourcePool = new Queue<AudioSource>();
    private const int initialPoolSize = 10;

    // Reference to the SoundEffectData ScriptableObject
    public SoundEffectData soundEffectData;

    // Dictionary to store audio clips with their corresponding SoundEffect enum
    private Dictionary<SoundEffect, List<AudioClip>> audioClipDictionary = new Dictionary<SoundEffect, List<AudioClip>>();

    protected override void Awake()
    {
        base.Awake();
        // Populate the dictionary from the SoundEffectData ScriptableObject
        LoadAudioClipsFromScriptableObject();

        // Initialize the object pool with audio sources
        for (int i = 0; i < initialPoolSize; i++)
        {
            CreateAudioSource();
        }
    }

    private void LoadAudioClipsFromScriptableObject()
    {
        if (soundEffectData == null)
        {
            Debug.LogError("SoundEffectData is not assigned in the AudioManager!");
            return;
        }

        AudioClip[] audioClips = soundEffectData.audioClips;
        SoundEffect[] soundEffects = (SoundEffect[])System.Enum.GetValues(typeof(SoundEffect));

        if (audioClips.Length != soundEffects.Length)
        {
            Debug.LogError("The number of audio clips in SoundEffectData does not match the number of SoundEffect enum elements!");
            return;
        }

        for (int i = 0; i < audioClips.Length; i++)
        {
            SoundEffect soundEffect = soundEffects[i];
            if (!audioClipDictionary.ContainsKey(soundEffect))
            {
                audioClipDictionary.Add(soundEffect, new List<AudioClip>());
            }
            audioClipDictionary[soundEffect].Add(audioClips[i]);
        }
    }

    private void CreateAudioSource()
    {
        // Create a new GameObject to hold the AudioSource component
        GameObject obj = new GameObject("AudioSource");
        obj.transform.SetParent(transform);

        // Add AudioSource component and disable it to be used later
        AudioSource audioSource = obj.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSourcePool.Enqueue(audioSource);
    }

    public void Play2DSoundEffect(SoundEffect soundEffect, float volume = 1.0f, float pitchMin = 1.0f, float pitchMax = 1.0f)
    {
        PlaySoundEffect(soundEffect, Vector3.zero, volume, pitchMin, pitchMax, 0f);
    }

    public void Play3DSoundEffect(SoundEffect soundEffect, Vector3 position, float volume = 1.0f, float pitchMin = 1.0f, float pitchMax = 1.0f, float minDistance = 1.0f, float maxDistance = 10.0f)
    {
        PlaySoundEffect(soundEffect, position, volume, pitchMin, pitchMax, 1f, minDistance, maxDistance);
    }

    private void PlaySoundEffect(SoundEffect soundEffect, Vector3 position, float volume, float pitchMin, float pitchMax, float spatialBlend, float minDistance = 1.0f, float maxDistance = 10.0f)
    {
        // Check if the audio clip exists in the dictionary
        if (!audioClipDictionary.ContainsKey(soundEffect))
        {
            Debug.LogWarning("Audio clip for SoundEffect '" + soundEffect + "' not found!");
            return;
        }

        List<AudioClip> clips = audioClipDictionary[soundEffect];
        if (clips.Count == 0)
        {
            Debug.LogWarning("No audio clips found for SoundEffect '" + soundEffect + "'!");
            return;
        }

        // Get a random audio clip from the list
        AudioClip clip = clips[Random.Range(0, clips.Count)];

        // Get an AudioSource from the pool and play the sound effect
        if (audioSourcePool.Count > 0)
        {
            AudioSource audioSource = audioSourcePool.Dequeue();
            audioSource.volume = volume;
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
            audioSource.clip = clip;

            // Set spatial properties for 3D audio
            audioSource.spatialBlend = spatialBlend;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;

            // Set the 3D position of the audio source if 3D audio
            if (spatialBlend > 0)
            {
                audioSource.transform.position = position;
            }

            audioSource.Play();

            // Return the AudioSource to the pool after the sound has finished playing
            StartCoroutine(ReturnAudioSourceToPool(audioSource));
        }
    }

    private System.Collections.IEnumerator ReturnAudioSourceToPool(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSourcePool.Enqueue(audioSource);
    }
}

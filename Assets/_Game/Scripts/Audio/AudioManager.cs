using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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
        GameObject obj = new GameObject("AudioSource");
        obj.transform.SetParent(transform);

        AudioSource audioSource = obj.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        obj.SetActive(false);

        audioSourcePool.Enqueue(audioSource);
    }

    public AudioSource Play2DSoundEffect(SoundEffect soundEffect, float volume = 1.0f, float pitchMin = 1.0f, float pitchMax = 1.0f, bool isOneShot = true)
    {
        return PlaySoundEffect(soundEffect, Vector3.zero, volume, pitchMin, pitchMax, 0f, isOneShot);
    }

    public AudioSource Play3DSoundEffect(SoundEffect soundEffect, Vector3 position, float volume = 1.0f, float pitchMin = 1.0f, float pitchMax = 1.0f, float minDistance = 1.0f, float maxDistance = 10.0f, bool isOneShot = true)
    {
        return PlaySoundEffect(soundEffect, position, volume, pitchMin, pitchMax, 1f, isOneShot, minDistance, maxDistance);
    }

    private AudioSource PlaySoundEffect(SoundEffect soundEffect, Vector3 position, float volume, float pitchMin, float pitchMax, float spatialBlend, bool isOneShot, float minDistance = 1.0f, float maxDistance = 10.0f)
    {
        if (!audioClipDictionary.ContainsKey(soundEffect))
        {
            Debug.LogWarning("Audio clip for SoundEffect '" + soundEffect + "' not found!");
            return null;
        }

        List<AudioClip> clips = audioClipDictionary[soundEffect];
        if (clips.Count == 0)
        {
            Debug.LogWarning("No audio clips found for SoundEffect '" + soundEffect + "'!");
            return null;
        }

        AudioClip clip = clips[Random.Range(0, clips.Count)];

        AudioSource audioSource = GetAvailableAudioSource();

        audioSource.volume = volume;
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.clip = clip;

        audioSource.spatialBlend = spatialBlend;
        audioSource.minDistance = minDistance;
        audioSource.maxDistance = maxDistance;

        audioSource.loop = !isOneShot;

        if (spatialBlend > 0)
        {
            audioSource.transform.position = position;
        }

        audioSource.gameObject.SetActive(true);

        audioSource.Play();

        StartCoroutine(ReturnAudioSourceToPool(audioSource));

        return audioSource;
    }

    private System.Collections.IEnumerator ReturnAudioSourceToPool(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.gameObject.SetActive(false);
        audioSourcePool.Enqueue(audioSource);
    }

    private AudioSource GetAvailableAudioSource()
    {
        if (audioSourcePool.Count > 0)
        {
            AudioSource audioSource = audioSourcePool.Dequeue();
            audioSource.gameObject.SetActive(true);
            return audioSource;
        }

        GameObject obj = new GameObject("AudioSource");
        obj.transform.SetParent(transform);
        AudioSource newAudioSource = obj.AddComponent<AudioSource>();
        newAudioSource.playOnAwake = false;

        return newAudioSource;
    }
}

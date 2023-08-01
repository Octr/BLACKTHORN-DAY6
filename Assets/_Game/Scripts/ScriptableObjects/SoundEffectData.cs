using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffectData", menuName = "AudioManager/SoundEffectData", order = 1)]
public class SoundEffectData : ScriptableObject
{
    public enum SoundEffect
    {
        // Add your sound effect names here
        ExampleSoundEffect1,
        ExampleSoundEffect2,
        // Add more sound effects as needed
    }

    public AudioClip[] audioClips;
}

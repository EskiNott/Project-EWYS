using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioType type;

    private void Update()
    {
        switch (type)
        {
            case AudioType.SoundEffect:
                audioSource.volume = AudioManager.Instance.SoundEffectVolume;
                break;
            case AudioType.BackgroundMusic:
                audioSource.volume = AudioManager.Instance.BackgroundMusicVolume;
                break;
        }

    }

    public enum AudioType
    {
        BackgroundMusic,
        SoundEffect
    }
}

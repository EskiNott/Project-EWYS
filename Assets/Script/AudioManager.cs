using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    public float SoundEffectVolume = 1;
    public float BackgroundMusicVolume = 1;

    public void SetSoundEffectVolume(float volume)
    {
        SoundEffectVolume = volume;
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        BackgroundMusicVolume = volume;
    }
}

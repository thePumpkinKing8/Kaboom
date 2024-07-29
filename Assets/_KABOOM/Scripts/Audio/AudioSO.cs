using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioSO", menuName = "SOs/AudioSO")]
public class AudioSO : ScriptableObject
{
    public AudioClip Clip;

    [Tooltip("Assign music and SFX to their own category mixer group.")]
    public AudioMixerGroup Mixer;

    public string AudioName;
    public float Volume = 1f;
    public float Pitch = 1f;
    public int Priority = 128;
    public bool Loop;
    public bool PlayOnAwake;

  
}

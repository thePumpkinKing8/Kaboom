using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioSO", menuName = "SOs/AudioSO")]
public class AudioSO : ScriptableObject
{
    public string AudioName;
    public AudioClip Clip;
    public float Volume = 1f;
    public float Pitch = 1f;
    public int Priority = 128;
    public bool Loop;
    public bool PlayOnAwake;
}

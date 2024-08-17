using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // One scriptable object per clip
    // To add a new sound, create a scriptable object using the AudioSO class and assign the values to it, then add it to the _audioSOs list in the inspector of the AudioManager object in the scene.
    // Make sure to assign sfx to an sfx mixer group and music to a music mixer group

    public static AudioManager Instance;

    [SerializeField]
    private AudioSO[] _audioSOs;

    private AudioSource _audioSource;

    private Dictionary<string, AudioSO> _audioPairs;

    private List<AudioSource> _audioPool;

    private int _poolSize;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioPairs = new Dictionary<string, AudioSO>();
        _poolSize = _audioSOs.Length; // Amount of audio sources we will instantiate depends on how many clips we have

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);

            foreach (var audioData in _audioSOs)
            {
                // Populate dictionary
                if (!_audioPairs.ContainsKey(audioData.AudioName))
                {
                    _audioPairs.Add(audioData.AudioName, audioData);
                }
            }
        }
        else if(Instance != this)
        {
            Destroy(Instance.gameObject);

            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }

        _audioPool = new List<AudioSource>();

        for (int i = 0; i < _poolSize; i++)
        {
            AudioSource pooledAudioSource = this.gameObject.AddComponent<AudioSource>();
            _audioPool.Add(pooledAudioSource);
        }
    }

    public AudioSource GetAudioSource()
    {
        // Gets an available audio source out of the pool
        foreach(AudioSource audioSource in _audioPool)
        {
            if(!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        return null;
    }

    public void PlayAudio(string audioName)
    {
        if (_audioPairs.TryGetValue(audioName, out var audioData))
        {
            _audioSource = GetAudioSource();

            // Gives the parameters from the scriptable object to the audio clip in question
            _audioSource.clip = audioData.Clip;
            _audioSource.volume = audioData.Volume;
            _audioSource.pitch = audioData.Pitch;
            _audioSource.priority = audioData.Priority;
            _audioSource.loop = audioData.Loop;
            _audioSource.playOnAwake = audioData.PlayOnAwake;
            _audioSource.outputAudioMixerGroup = audioData.Mixer;

            _audioSource.PlayOneShot(audioData.Clip);
        }
        else
        {
            Debug.Log("The clip" + audioName + "cannot be found.");
        }
    }

    public void PauseAudio(string audioName)
    {
        _audioSource.Pause();
    }

    public void UnPauseAudio(string audioName)
    {
        _audioSource.UnPause();
    }

    public void StopAudio(string audioName)
    {
        if(_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

}

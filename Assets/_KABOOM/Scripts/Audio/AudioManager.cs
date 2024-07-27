using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]
    private List<AudioSO> _audioSOs;

    private AudioSource _audioSource;

    private Dictionary<string, AudioSO> _audioPairs;


    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioPairs = new Dictionary<string, AudioSO>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

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
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayAudio(string audioName)
    {
        if(_audioPairs.TryGetValue(audioName, out var audioData))
        {
            // Gives the parameters from the scriptable object to the audio clip in question
            _audioSource.clip = audioData.Clip;
            _audioSource.volume = audioData.Volume;
            _audioSource.pitch = audioData.Pitch;
            _audioSource.priority = audioData.Priority;
            _audioSource.loop = audioData.Loop;
            _audioSource.playOnAwake = audioData.PlayOnAwake;

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

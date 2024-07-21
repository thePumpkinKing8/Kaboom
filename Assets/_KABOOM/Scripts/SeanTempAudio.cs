using System.Collections;
using System.Collections.Generic;
using _KABOOM.Scripts.PlayerScripts;
using Unity.VisualScripting;
using UnityEngine;

public class SeanTempAudio : MonoBehaviour
{
    [SerializeField] private PlayerActionsData playerActionsData;
    // Start is called before the first frame update
    private AudioSource _audioSource;
    [SerializeField] private AudioSource _footSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
        playerActionsData.PlayerJumps.AddListener(PlayAudio);
        playerActionsData.PlayerMovementEvent.AddListener(val =>
        {
            if(!_footSource.isPlaying)
                _footSource.Play();
            if(val == Vector2.zero)
                _footSource.Stop();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        _audioSource.Play();
    }
    
}

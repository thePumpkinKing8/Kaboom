using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerSettings _playerPhysicsSettings;
    public GameObject Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
            if(LevelManager.Instance.SpawnPoint != Vector3.zero)
            {
                _player.transform.position = LevelManager.Instance.SpawnPoint;
            }
        }
    }

    private GameObject _player;
    public PlayerSettings PlayerPhysicsSettings { get { return _playerPhysicsSettings; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //testscript
        if (Input.GetKeyDown(KeyCode.K)) 
        {
            LevelManager.Instance.ReLoadLevel();
        }
    }
}

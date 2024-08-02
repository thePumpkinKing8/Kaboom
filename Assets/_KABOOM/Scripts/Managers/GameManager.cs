using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerSettings _playerPhysicsSettings;
    public PlayerSettings PlayerPhysicsSettings { get { return _playerPhysicsSettings; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

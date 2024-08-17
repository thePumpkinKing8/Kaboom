using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public LevelEventData EventData;
    [SerializeField] private int _keysNeeded = 3;
    private int _keysCollected = 0;
    [SerializeField] private int _levelIndex = 1;
    public Vector3 SpawnPoint { get; private set; }


    private void OnEnable()
    {
        _persistant = true;
        EventData.KeyCollectedEvent.AddListener(KeyCollected);
        EventData.LevelCompleteEvent.AddListener(LoadLevel);
        EventData.PlayerKilledEvent.AddListener(ReLoadLevel);
        //LoadLevel();
    }
    private void Start()
    {
        Debug.Log("?");
        SpawnPoint = Vector3.zero;
    }
    //sets up parameters for level completion and othe level specific settings
    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      
        _levelIndex++;
        SpawnPoint = Vector3.zero;
    }

    public void ReLoadLevel(string str = "Reload")
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        EventData.ReloadLevelEvent.Invoke();
        
    }


    private void KeyCollected(string str = "KeyCollected")
    {
        _keysCollected++;
    }

    public void NewSpawnPoint(Vector3 point)
    {
        SpawnPoint = point;
    }

    public bool AllKeysCollected() => _keysCollected >= _keysNeeded;
}

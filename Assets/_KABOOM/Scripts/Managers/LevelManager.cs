using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public LevelEventData EventData { get; private set;}
    private int _keysNeeded = 0;
    private int _keysCollected = 0;
    [SerializeField] private int _levelIndex = 0;
    private LevelSO _currentLevel;
    [SerializeField] private LevelSO[] _levels;


    private void Awake()
    {
        EventData.KeyCollectedEvent.AddListener(KeyCollected);
        EventData.LevelCompleteEvent.AddListener(LoadLevel);
    }
    //sets up parameters for level completion and othe level specific settings
    public void LoadLevel()
    {
        LevelSO _currentLevel = _levels[_levelIndex];
        _keysNeeded = _currentLevel.KeysToWin;
        SceneManager.LoadScene(_currentLevel.LevelName);
        EventData.NewLevelStartEvent.Invoke(_currentLevel.BGMName);
        _levelIndex++;
    }

    private void KeyCollected(string str)
    {
        _keysCollected++;
    }

    public bool AllKeysCollected() => _keysCollected >= _keysNeeded;
}

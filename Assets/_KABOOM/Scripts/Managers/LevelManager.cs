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
        EventData.ToMenu.AddListener(Unload);
        //LoadLevel();
    }
    private void Start()
    {
        AudioManager.Instance.PlayAudio("Level1");
        SpawnPoint = Vector3.zero;
    }
    //sets up parameters for level completion and othe level specific settings
    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.Instance.StopAudio("Level1");
        AudioManager.Instance.StopAudio("Level2");
        _levelIndex++;
        switch(_levelIndex)
        {
            case 1:
                AudioManager.Instance.PlayAudio("Level1");
                break;
            case 2:
                AudioManager.Instance.PlayAudio("Level2");
                break;
            default:
                _levelIndex = 1;
                break;
        }

        SpawnPoint = Vector3.zero;
    }

    public void ReLoadLevel(string str = "Reload")
    {
        StartCoroutine(PlayerDeath());
    }


    private void KeyCollected(string str = "KeyCollected")
    {
        _keysCollected++;
    }

    public void NewSpawnPoint(Vector3 point)
    {
        SpawnPoint = point;
    }

    IEnumerator PlayerDeath()
    {
        InputManager.Instance.Input.Player.Disable();
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        EventData.ReloadLevelEvent.Invoke();
       
        InputManager.Instance.Input.Player.Enable();
        yield return null;
    }

    private void Unload()
    {
        AudioManager.Instance.StopAudio("Level1");
        AudioManager.Instance.StopAudio("Level2");
        Destroy(Instance);
        Destroy(gameObject);
    }
    public bool AllKeysCollected() => _keysCollected >= _keysNeeded;
}

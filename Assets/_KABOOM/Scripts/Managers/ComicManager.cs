using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicManager : MonoBehaviour
{
    // ALL SCENES USED MUST BE ADDED TO BUILD SETTINGS.

    [SerializeField]
    private string _nextComicScene; // Next comic page scene. If the next page is the last one, leave this blank.

    [SerializeField]
    private string _sceneToSkipTo; // Either start the game or go back to main menu (if we're looking at the comic from the main menu)

    [SerializeField]
    private string _lastScene; // If the next scene is the last one, fill this out and leave _nextComicScene blank.

    private GameObject _loadingScreen;

    private void Awake()
    {
        _loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen");

        _loadingScreen.SetActive(false);
    }

    public void NextComicScene()
    {
        string nextScene;

        // If there is something in "_nextComicScene" in the inspector, load that scene.
        if (string.IsNullOrEmpty(_lastScene) && !string.IsNullOrEmpty(_nextComicScene))
        {
            nextScene = _nextComicScene;
            SceneManager.LoadScene(nextScene);

        }

        // If there is something in "_lastScene" in the inspector, load that scene and use the loading screen.
        else if(!string.IsNullOrEmpty(_lastScene) && string.IsNullOrEmpty(_nextComicScene))
        {
            nextScene = _lastScene;
            StartCoroutine(LoadSceneAsync(_lastScene));
        }
        
        else if(string.IsNullOrEmpty(_lastScene) && string.IsNullOrEmpty(_nextComicScene))
        {
            Debug.LogError("No scene to progress to. Please check the serialized fields on the [ComicManager] object in ComicManager.cs.");
        }

    }

    public void SceneToSkipTo()
    {
        if (string.IsNullOrEmpty(_sceneToSkipTo))
        {
            Debug.LogError("No scene to progress to. Please check the serialized fields on the [ComicManager] object in ComicManager.cs.");

            return;
        }

        StartCoroutine(LoadSceneAsync(_sceneToSkipTo));

    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Load by scene name

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        _loadingScreen.SetActive(true);

        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    #region Variables

    private int _firstSceneIndex = 1; // The first scene that should load via the build index

    [SerializeField]
    private GameObject _mainMenu;

    [SerializeField]
    private GameObject _newGameMenu;

    [SerializeField]
    private GameObject _continueMenu;

    [SerializeField]
    private GameObject _settingsMenu;

    [SerializeField]
    private GameObject _howToPlayMenu;

    [SerializeField]
    private GameObject _loadingScreen;

    #endregion

    private void Awake()
    {
        CloseMenus();
    }

    #region Coroutines
    // I have two of these. Shows loading screen while the game is loading.
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Load by scene name

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        _loadingScreen.SetActive(true);

        yield return null;
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        // Load by scene index

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        _loadingScreen.SetActive(true);

        yield return null;
    }
    #endregion

    #region Button functions

    // When the game starts
    public void OnNewGameButton()
    {
        _newGameMenu.SetActive(true);
    }

    private void LoadingScreen()
    {
        // Use this once we have stuff for starting a new game
        StartCoroutine(LoadSceneAsync(_firstSceneIndex));
    }

    public void OnContinueButton()
    {
        _continueMenu.SetActive(true);
        // Will need another function with logic for continuing a saved game
    }
    public void OnSettings()
    {
        _settingsMenu.SetActive(true);
    }
    public void OnQuit()
    {
        Application.Quit(); // This only works in the build
        Debug.Log("Quitting game...");
    }

    public void OnHowToPlay()
    {
        _howToPlayMenu.SetActive(true);
    }
    public void CloseMenus()
    {
        // For closing menus within the main menu
        if(_newGameMenu.activeInHierarchy || _settingsMenu.activeInHierarchy || _continueMenu.activeInHierarchy || _howToPlayMenu.activeInHierarchy)
        {
            _newGameMenu.SetActive(false);
            _settingsMenu.SetActive(false);
            _howToPlayMenu.SetActive(false);
            _continueMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }
    #endregion
}

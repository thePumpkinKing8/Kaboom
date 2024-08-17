using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    #region Variables

    private int _firstSceneIndex = 1; // The first scene that should load via the build index

    [SerializeField]
    private GameObject _mainMenu;

    [SerializeField] private GameObject _menuTopButton;

    [SerializeField]
    private GameObject _optionsMenu;

    [SerializeField] private GameObject _settingsTopButton;

    [SerializeField]
    private GameObject _loadingScreen;

    [SerializeField]
    private string _storySceneName = "MenuComic1";

    [SerializeField]
    private GameObject _controlsScreen;

    private EventSystem _eventSystem;
    #endregion

    private void Awake()
    {
        CloseMenus();
        _eventSystem = FindAnyObjectByType<EventSystem>();
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
    public void OnPlayGame()
    {
        StartCoroutine(LoadSceneAsync(_firstSceneIndex));
    }

    public void Options()
    {
        StartCoroutine(OnOptions());
    }
    public void CloseOptions()
    {
        StartCoroutine(CloseMenus());
    }
    IEnumerator OnOptions()
    {
        _optionsMenu.SetActive(true);
        _mainMenu.SetActive(false);
        _eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _eventSystem.SetSelectedGameObject(_settingsTopButton);
        yield return null ;
    }

    IEnumerator Controls()
    {
        _controlsScreen.SetActive(true);
        _eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _eventSystem.SetSelectedGameObject(_controlsScreen);    
        yield return null ;
    }
    public void OnControls()
    {
        StartCoroutine(Controls());
    }
    public void OnQuit()
    {
        Application.Quit(); // This only works in the build
        Debug.Log("Quitting game...");
    }
    public void OnStory()
    {
        SceneManager.LoadScene(_storySceneName); // Load the comic scene (must be the menu specific one)
    }
    IEnumerator CloseMenus()
    {
        // For closing menus within the main menu
        if(_optionsMenu.activeInHierarchy)
        {
            _optionsMenu.SetActive(false);

            _mainMenu.SetActive(true);
            _eventSystem.SetSelectedGameObject(null);
            yield return new WaitForEndOfFrame();
            _eventSystem.SetSelectedGameObject(_menuTopButton);
        }
        yield return null;
    }
    #endregion
}

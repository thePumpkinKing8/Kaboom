using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenuManager : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField] private GameObject _controlsMenu;
    [SerializeField] private GameObject _topButton;
    private EventSystem _eventSystem;
    public bool IsPaused { get; private set; }

    #endregion

    private void Awake()
    {
        _eventSystem = EventSystem.current;
        _pauseMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        InputManager.Instance.ActionsData.PlayerPauseEvent.AddListener(PauseInput);
    }

    public void PauseInput()
    {
        if(IsPaused)
        {
            OnResume();
        }
        else
        {
            OnPause();
        }
    }
    public void OnPause()
    {
        Time.timeScale = 0f;

        _pauseMenu.SetActive(true);
        StartCoroutine(OpenMenu());
        InputManager.Instance.Input.Player.Disable();
        IsPaused = true;
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);
        InputManager.Instance.Input.Player.Enable();
        Time.timeScale = 1f;

        IsPaused = false;
    }
    public void OnControls()
    {
        StartCoroutine(OpenControls());
        _controlsMenu.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void CloseMenus()
    {
        StartCoroutine(OpenMenu());
        _pauseMenu.SetActive(true);
        _controlsMenu.SetActive(false);
    }

    public void OnMainMenu()
    {
        // Showing the "do you want to quit" warning when the player selects Main Menu
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator OpenControls()
    {
        _eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _eventSystem.SetSelectedGameObject(_controlsMenu);
        yield return null;
    }
    IEnumerator OpenMenu()
    {
        _eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        _eventSystem.SetSelectedGameObject(_topButton);
        yield return null;
    }

    


}

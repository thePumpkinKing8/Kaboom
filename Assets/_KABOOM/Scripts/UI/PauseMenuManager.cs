using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _quitWarning;

    [SerializeField]
    private GameObject _saveMenu;

    [SerializeField]
    private GameObject _settingsMenu;

    public bool IsPaused { get; private set; }

    #endregion

    private void Awake()
    {
        CloseMenus();
    }

    public void OnPause()
    {
        Time.timeScale = 0f;

        _pauseMenu.SetActive(true);

        IsPaused = true;
    }

    public void OnResume()
    {
        _pauseMenu.SetActive(false);

        Time.timeScale = 1f;

        IsPaused = false;
    }

    public void CloseMenus()
    {
        if(_quitWarning.activeInHierarchy || _saveMenu.activeInHierarchy || _settingsMenu.activeInHierarchy)
        {
            // For closing menus within the pause menu
            _settingsMenu.SetActive(false);
            _saveMenu.SetActive(false);
            _quitWarning.SetActive(false);
            _pauseMenu.SetActive(true);
        }
    }

    public void OnMainMenu()
    {
        // Showing the "do you want to quit" warning when the player selects Main Menu
        _quitWarning.SetActive(true);
    }

    public void OnYesQuit()
    {
        // "No" should be linked to "close menus"

        Time.timeScale = 1f; // Required to avoid bugs

        IsPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void OnSave()
    {
        _saveMenu.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void OnSettings()
    {
        _pauseMenu.SetActive(false);

        _settingsMenu.SetActive(true);
    }


}

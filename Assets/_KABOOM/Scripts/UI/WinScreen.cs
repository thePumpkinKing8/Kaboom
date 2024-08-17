using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinScreen : MonoBehaviour
{
    private void Awake()
    {
        LevelManager.Instance.EventData.ToMenu.Invoke();
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

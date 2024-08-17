using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreen : MonoBehaviour
{
    [SerializeField] private int _secondsToWait;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_secondsToWait);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }
}

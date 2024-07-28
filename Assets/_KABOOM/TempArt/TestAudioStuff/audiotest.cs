using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class audiotest : MonoBehaviour
{
    public UnityEvent PlayBwop;
    public UnityEvent PlayCaw;
    public UnityEvent PlayUnusualMusic;

    private void Start()
    {
        StartCoroutine(Wait());

    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            PlayCaw.Invoke();
        }

        if(Input.GetMouseButtonDown(0))
        {
            PlayBwop.Invoke();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        PlayUnusualMusic.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class clicking : MonoBehaviour
{
    public UnityEvent CLICKWOW;
    public UnityEvent notclicking;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            CLICKWOW.Invoke();
        }
        if(Input.GetMouseButtonUp(0))
        {
            notclicking.Invoke();
        }

    }
}

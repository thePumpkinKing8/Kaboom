using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance; //all private fields for properties are denoted with an underscore in front of the name
    public static T Instance //this is our property and it works by, when we first use it finding the object of the type and assigning it to the instance, and after that it simply returns the instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                DontDestroyOnLoad(_instance);
            }
            
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (this != _instance)
            {
                // If there is already an instance of this Singleton, destroy this one.
                Destroy(gameObject);
            }
        }

        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }

}
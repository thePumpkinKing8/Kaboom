using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIEvents : Singleton<UIEvents>
{
    public UnityEvent<float> OnHealthChangedUI;
    public UnityEvent OnKeyPickupUI;

}

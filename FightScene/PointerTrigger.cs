using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerTrigger : MonoBehaviour
{
    [SerializeField] Collider Collider;

    private Action EnterCallback;
    private Action ExitCallback;


    public void Init(Action enterCallback, Action exitCallback) 
    {
        EnterCallback = enterCallback;
        ExitCallback = exitCallback;
    }

    private void OnMouseEnter()
    {
        EnterCallback?.Invoke();
    }

    private void OnMouseExit()
    {
        ExitCallback?.Invoke();
    }
}

using UnityEngine;
using System.Collections;
using System;

public class CallbackHolder
{
    private Action Callback;

    public void SetCallback(Action callback)
    {
        Callback = callback;
    }

    public void Invoke()
    {
        Action action = Callback;
        Callback = null;
        action?.Invoke();
    }
}

using UnityEngine;
using System.Collections;
using System;

public interface IPhase
{
    void StartPhase(Action onComplete);
}

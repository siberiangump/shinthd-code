using System;
using UnityEngine;

public class OpponentTurnPhase : MonoBehaviour, IPhase
{
	public void Init() 
	{
	}

	void IPhase.StartPhase(Action onComplete)
	{
		onComplete.Invoke();
	}
}

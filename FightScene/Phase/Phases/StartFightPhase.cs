using System;
using UnityEngine;

public class StartFightPhase : MonoBehaviour, IPhase,
	Injection<FightState>
{
	FightState FightState;

	public void Init() 
	{
	}

	void IPhase.StartPhase(Action onComplete)
	{
		onComplete.Invoke();
	}

	#region Injections
	void Injection<FightState>.Inject(FightState data) => FightState = data;
	#endregion
}

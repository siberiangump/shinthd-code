using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnPhase : MonoBehaviour, IPhase,
	Injection<APPanel>,
	Injection<FightState>
{
	APPanel APPanel;
	FightState FightState;
	[SerializeField] HeroActionIntent HeroActionIntent; 

	public void Init() 
	{
	}

	void IPhase.StartPhase(Action onComplete)
	{
		HeroActionIntent.Action(HeroActionDone);
	}

	private void HeroActionDone() 
	{
		StartCoroutine(OnHeroActionDone());
	}

	private IEnumerator OnHeroActionDone() 
	{
		yield return new WaitForEndOfFrame();
		HeroActionIntent.Action(HeroActionDone);
	}

	private void StartTurn() 
	{
		FightState.AP = 3;
		APPanel.SetAP(FightState.AP);
	}

	private void UnlockPlayerForTurn()
	{

	}

	#region Injections
	void Injection<APPanel>.Inject(APPanel data) => APPanel = data;
	void Injection<FightState>.Inject(FightState data) => FightState = data;
	#endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPhaseSolver : MonoBehaviour, IPhaseSolver<FightPhaseEnum>
{
	[SerializeField] FightState FightState;

	public void Init(FightState fightState) 
	{
		FightState = fightState;
	}

	FightPhaseEnum IPhaseSolver<FightPhaseEnum>.GetNext(FightPhaseEnum phase)
	{
		switch (phase)
		{
			case FightPhaseEnum.StartFight: return FightState.Turn == TurnState.Player ? FightPhaseEnum.PlayerTurn : FightPhaseEnum.OpponentTurn;
			case FightPhaseEnum.PlayerTurn: return FightPhaseEnum.OpponentTurn;
			case FightPhaseEnum.OpponentTurn: return FightPhaseEnum.PlayerTurn;
			case FightPhaseEnum.EndFight: return FightPhaseEnum.EndFight;
			default: return FightPhaseEnum.EndFight;
		}
	}
}

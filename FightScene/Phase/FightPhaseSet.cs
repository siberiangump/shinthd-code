using UnityEngine;

public class FightPhaseSet : MonoBehaviour, IPhaseSet<FightPhaseEnum>
{
	[SerializeField] StartFightPhase StartFightPhase;
	[SerializeField] PlayerTurnPhase PlayerTurnPhase;
	[SerializeField] OpponentTurnPhase OpponentTurnPhase;
	[SerializeField] EndFightPhase EndFightPhase;

	IPhase IPhaseSet<FightPhaseEnum>.GetPhase(FightPhaseEnum currentPhase)
	{
		switch (currentPhase)
		{
			case FightPhaseEnum.StartFight: return StartFightPhase;
			case FightPhaseEnum.PlayerTurn: return PlayerTurnPhase;
			case FightPhaseEnum.OpponentTurn: return OpponentTurnPhase;
			case FightPhaseEnum.EndFight: return EndFightPhase;
			default: return null;
		}
	}
}
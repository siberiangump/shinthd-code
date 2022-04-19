using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPhaseRunner : AbstractPhaseRunner<FightPhaseEnum>
{
	[SerializeField] FightPhaseSet FightPhaseSet;
	[SerializeField] FightPhaseSolver FightPhaseSolver;
	[SerializeField] FightPhaseEnum FightPhaseEnum;

	public void Init(FightState fightState)
	{
		FightPhaseSolver.Init(fightState);
	}

	protected override IPhaseSet<FightPhaseEnum> PhaseSet => FightPhaseSet;
	protected override IPhaseSolver<FightPhaseEnum> PhaseSolver => FightPhaseSolver;
	protected override FightPhaseEnum CurrentPhase { get => FightPhaseEnum; set => FightPhaseEnum = value; }

}

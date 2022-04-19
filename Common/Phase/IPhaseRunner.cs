using UnityEngine;
using System.Collections;

public interface IPhaseRunner<PhaseEnum>
{
    IPhaseSet<PhaseEnum> PhaseSet { get; }
    IPhaseSolver<PhaseEnum> PhaseSolver { get; }
    PhaseEnum CurrentPhase { get; set; }
}

public static class IPhaseRunnerExtantion 
{
    public static void Run<PhaseEnum>(this IPhaseRunner<PhaseEnum> phaseRunner)
    {
        phaseRunner.GotoPhase<PhaseEnum>(phaseRunner.CurrentPhase);
    }

    private static void GotoPhase<PhaseEnum>(this IPhaseRunner<PhaseEnum> phaseRunner, PhaseEnum phaseEnum)
    {
        phaseRunner.CurrentPhase = phaseEnum;
        Debug.Log($"GotoPhase : {phaseRunner.CurrentPhase}");
        phaseRunner.PhaseSet.GetPhase(phaseEnum).StartPhase(phaseRunner.GotoNext);
    }

    private static void GotoNext<PhaseEnum>(this IPhaseRunner<PhaseEnum> phaseRunner)
    {
        phaseRunner.GotoPhase<PhaseEnum>(phaseRunner.PhaseSolver.GetNext(phaseRunner.CurrentPhase));
    }
}
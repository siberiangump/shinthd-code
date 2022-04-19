using UnityEngine;
using System.Collections;

public abstract class AbstractPhaseRunner<PhaseEnum> : MonoBehaviour
{
    protected abstract IPhaseSet<PhaseEnum> PhaseSet { get; }
    protected abstract IPhaseSolver<PhaseEnum> PhaseSolver { get; }
    protected abstract PhaseEnum CurrentPhase { get; set; }

    public void Run()
    {
        GotoPhase(CurrentPhase);
    }

    private void GotoPhase(PhaseEnum phaseEnum)
    {
        CurrentPhase = phaseEnum;
        Debug.Log($"GotoPhase : {CurrentPhase}");
        PhaseSet.GetPhase(phaseEnum).StartPhase(GotoNext);
    }

    private void GotoNext()
    {
        GotoPhase(PhaseSolver.GetNext(CurrentPhase));
    }
}

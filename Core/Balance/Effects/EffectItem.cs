using UnityEngine;

[System.Serializable]
public struct EffectItem
{
    [Header("When it Happens")]
    public TriggerType Trigger;

    [Header("What will Happened")]
    public EffectType Effect;
    public int EffectId;
    
    [Header("Pattern Restriction")]
    public Target Target;
    public PatternType Pattern;

    [Header("Target Selection")]
    public TargetSelectionType TargetSelection;
    public TargetAmount TargetAmount;

    public static EffectItem Empty()
    {
        return new EffectItem
        {
            EffectId = -1,
            Effect = EffectType.None,
            Pattern = PatternType.None,
            Target = Target.None,
            Trigger = TriggerType.None,
            TargetSelection = TargetSelectionType.None
        };
    }
}
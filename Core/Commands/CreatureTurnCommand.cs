
public class CreatureTurnCommand
{
    public SelectionData ExecuteCreatureTurn(TargetSpot fromSpot, FightState sessionModel, BalanceModel cardBalanceModel, 
        AggregatedEffectCommand aggregatedEffectCommand,
        PatternMatchCommand patternMatchCommand, TargetSelectionCommand targetSelectionCommand, IFieldOperations fieldOperations)
    {
        SelectionData selectionData = GetActionTarget(fromSpot, sessionModel, cardBalanceModel, patternMatchCommand, targetSelectionCommand, fieldOperations);
        Character battleFieldCreatureModel = sessionModel.GetCharacterAtSpot(fromSpot);
        sessionModel = aggregatedEffectCommand.DoEffect(battleFieldCreatureModel, selectionData.EffectItem, selectionData.Effect, selectionData.EffectTargets, sessionModel, cardBalanceModel);
        return selectionData;
    }

    public SelectionData GetActionTarget(TargetSpot fromSpot, FightState sessionModel, BalanceModel cardBalanceModel,
        PatternMatchCommand patternMatchCommand, TargetSelectionCommand targetSelectionCommand, IFieldOperations fieldOperations)
    {
        return new SelectionData()
        { TargetSpot = fromSpot, EffectItem = EffectItem.Empty(), Pattern = PatternMatchResult.Empty(), EffectTargets = PatternMatchResult.Empty(), Effect = null, Creature = null, BattleFieldCreature = null };

    }

    private SelectionData GetSelectionData(TargetSpot fromSpot, IActionModel cardModel, FightState sessionModel, BalanceModel cardBalanceModel,
        PatternMatchCommand patternMatchCommand, TargetSelectionCommand targetSelectionCommand, IFieldOperations fieldOperations)
    {
        ICreatureModel creature = cardModel as ICreatureModel;
        if (creature == null)
            return new SelectionData()
            { TargetSpot = fromSpot, EffectItem = EffectItem.Empty(), Pattern = PatternMatchResult.Empty(), EffectTargets = PatternMatchResult.Empty(), Effect = null, Creature = null, BattleFieldCreature = null };
        EffectItem effectItem = creature.Action();
        IEffect effect = cardBalanceModel.Effects.GetEffect(effectItem);
        PatternMatchResult patternMatchResult = patternMatchCommand.GetTargetsFromSpot(fromSpot, creature.Action().Target, creature.Action().Pattern);
        PatternMatchResult effectTargetsResult = targetSelectionCommand.Select(fromSpot, creature.Action(), patternMatchResult, sessionModel, cardBalanceModel, fieldOperations);
        return new SelectionData()
        { TargetSpot = fromSpot, EffectItem = effectItem, Pattern = patternMatchResult, EffectTargets = effectTargetsResult, Creature = creature, Effect = effect };
    }
}

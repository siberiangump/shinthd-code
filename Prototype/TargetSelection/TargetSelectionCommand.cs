using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class TargetSelectionCommand
{
    public PatternMatchResult Select(TargetSpot targetSpot, EffectItem effect, PatternMatchResult patternMatch,
        FightState fight, BalanceModel cardBalance, IFieldOperations fieldOperations)
    {
        PatternMatchResult result = PatternMatchResult.Empty();
        Party player = patternMatch.Revers ? fight.GetOpponent() : fight.GetPlayer();
        Party target = patternMatch.Revers ? fight.GetPlayer() : fight.GetOpponent();
        if (patternMatch.Revers)
            patternMatch = patternMatch.DoReverse();
        switch (effect.TargetSelection)
        {
            case TargetSelectionType.Melee:
                result = Melee(targetSpot, effect.TargetAmount, patternMatch, player, target, fieldOperations);
                break;
            case TargetSelectionType.Ranger:
                result = Ranged(targetSpot, effect.TargetAmount, patternMatch, target, fieldOperations);
                break;
            case TargetSelectionType.Assassin:
                result = Assassin(effect.TargetAmount, patternMatch, target, cardBalance);
                break;
            case TargetSelectionType.AllInRange:
                result = patternMatch;
                result.CreaturesFriendly = ReturnNonEmptySpots(patternMatch.CreaturesFriendly, player.Field.Spot).ToArray();
                result.CreaturesOpponent = ReturnNonEmptySpots(patternMatch.CreaturesOpponent, target.Field.Spot).ToArray();
                break;
            case TargetSelectionType.None:
                PatternMatchResult.Empty();
                break;
            default:
                break;
        }
        if (patternMatch.Revers)
            result = result.DoReverse();
        return result;
    }

    public PatternMatchResult Melee(TargetSpot targetSpot, TargetAmount amountType,
        PatternMatchResult patternMatch, Party player, Party opponent, IFieldOperations fieldOperations)
    {
        PatternMatchResult result = PatternMatchResult.Empty();
        int[] friendlyBlocker = fieldOperations.GetMeleeBlocker(targetSpot.Position);
        result.BlockerFrendly = friendlyBlocker;
        if (IfAnyAliveOnSpots(friendlyBlocker, player.Field.Spot))
            return result;
        
        int[] priority = fieldOperations.GetMeleePriority(targetSpot.Position);

        int amount = (int)amountType;
        int[] targets = GetFirstInPriorityFrom(priority, patternMatch.CreaturesOpponent, amount, opponent.Field.Spot);

        result.CreaturesOpponent = targets;
        
        return result;
    }

    public PatternMatchResult Ranged(TargetSpot targetSpot, TargetAmount amountType,
        PatternMatchResult patternMatch, Party opponent, IFieldOperations fieldOperations)
    {
        PatternMatchResult result = PatternMatchResult.Empty();
        int[] priority = fieldOperations.GetRangePriority(targetSpot.Position);
        int amount = (int)amountType;
        int[] targets = GetFirstInPriorityFrom(priority, patternMatch.CreaturesOpponent, amount, opponent.Field.Spot);

        result.CreaturesOpponent = targets;
        
        return result;
    }

    public PatternMatchResult Assassin(TargetAmount amountType, PatternMatchResult patternMatch, Party opponent, BalanceModel cardBalanceModel)
    {
        PatternMatchResult result = PatternMatchResult.Empty();
        int amount = (int)amountType;

        int[] targets = GetWeakestCreatures(patternMatch.CreaturesOpponent, amount, opponent.Field.Spot, opponent.Characters);

        result.CreaturesOpponent = targets;
        
        return result;
    }

    private bool IsAttackHero(bool inPatter, int amount) 
    {
        return amount > 0 && inPatter;
    }

    private int[] GetFirstInPriorityFrom(int[] priority, int[] from, int amount, int[] creatures)
    {
        List<int> targets = new List<int>();

        for (int i = 0; i < priority.Length; i++)
        {
            int spot = priority[i];
            if (amount <= 0)
                break;

            if (from.Contains(spot) == false)
                continue;

            // is empty
            if (creatures[spot] == -1)
                continue;

            amount--;
            targets.Add(spot);
        }

        return targets.ToArray();
    }

    private bool IfAnyAliveOnSpots(int[] targetSpots, int[] fieldSpots)
    {
        for (int i = 0; i < targetSpots.Length; i++)
            // ВЫГЛЯДИТ ХЕРОВО ВРОДЕ ДОЛЖНО БЫТЬ НАОБОРОТ
            if (fieldSpots[targetSpots[i]] == -1)
                return true;
        return false;
    }

    private List<int> ReturnNonEmptySpots(int[] targetSpots, int[] fieldSpots) 
    {
        List<int> nonEmpty = new List<int>();
        for (int i = 0; i < targetSpots.Length; i++)
            if (fieldSpots[targetSpots[i]] != ModelConstants.Empty)
                nonEmpty.Add(targetSpots[i]);
        return nonEmpty;
    }

    private int[] GetWeakestCreatures(int[] from, int amount, int[] spots, Character[] characters)
    {
        if (amount == 0)
            return new int[0];

        List<IndexAndHealth> healthIndexList = new List<IndexAndHealth>();

        for (int i = 0; i < spots.Length; i++)
        {
            Character target = characters[spots[i]];

            if (from.Contains(i) == false)
                continue;

            if (spots[i] == ModelConstants.Empty)
                continue;

            int health = target.Health - target.TakenDamage;

            healthIndexList.Add(new IndexAndHealth() { Index = i, Health = health });
        }

        if (healthIndexList.Count == 0)
            return new int[0];

        healthIndexList.Sort();
        healthIndexList = healthIndexList.GetRange(0, amount);
        int[] select = new int[healthIndexList.Count];

        for (int i = 0; i < healthIndexList.Count; i++)
            select[i] = healthIndexList[i].Index;

        return select;
    }

    private struct IndexAndHealth : IComparer<IndexAndHealth>, IComparable<IndexAndHealth>
    {
        public int Index;
        public int Health;

        public int CompareTo(IndexAndHealth other) =>  this.Health == other.Health? 1 : this.Health > other.Health? 1 : -1;

        int IComparer<IndexAndHealth>.Compare(IndexAndHealth x, IndexAndHealth y) => x.Health == y.Health ? 1 : x.Health > y.Health ? 1 : -1;
    }
}

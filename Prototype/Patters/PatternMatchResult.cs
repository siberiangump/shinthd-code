using System.Collections.Generic;

public struct PatternMatchResult
{
    public bool Revers;
    public int[] CreaturesFriendly;
    public int[] CreaturesOpponent;
    public int[] BlockerFrendly;
    public int[] BlockerOpponent;

    public static PatternMatchResult Empty() =>
        new PatternMatchResult() { 
            Revers = false, 
            CreaturesFriendly = new int[0], 
            CreaturesOpponent = new int[0], 
            BlockerFrendly = new int[0], 
            BlockerOpponent = new int[0]
        };
}

public static class PatternMatchResultOperations
{
    public static Targets SelectCreatures(this PatternMatchResult patternMatchResult, FightState fight)
    {
        List<Character> targetCreatures = new List<Character>();
        if (patternMatchResult.CreaturesFriendly.Length > 0)
            selectFrom(patternMatchResult.CreaturesFriendly, fight.GetPlayer().Field.Spot, fight.GetPlayer().Characters);
        if (patternMatchResult.CreaturesOpponent.Length > 0)
            selectFrom(patternMatchResult.CreaturesOpponent, fight.GetOpponent().Field.Spot, fight.GetOpponent().Characters);

        return new Targets
        {
            Creatures = targetCreatures.ToArray()
        };

        void selectFrom(int[] selection, int[] fieldSpots, Character[] characters)
        {
            for (int i = 0; i < selection.Length; i++)
            {
                int charIndex = fieldSpots[selection[i]];
                if (charIndex == ModelConstants.Empty)
                    continue;
                targetCreatures.Add(characters[charIndex]);
            }
        }
    }

    public static PatternMatchResult DoReverse(this PatternMatchResult patternMatch)
    {
        return new PatternMatchResult
        {
            Revers = true,
            CreaturesFriendly = patternMatch.CreaturesOpponent,
            CreaturesOpponent = patternMatch.CreaturesFriendly,
            BlockerFrendly = patternMatch.BlockerOpponent,
            BlockerOpponent = patternMatch.BlockerFrendly,
        };
    }
}

public struct Targets
{
    public Character[] Creatures;
}
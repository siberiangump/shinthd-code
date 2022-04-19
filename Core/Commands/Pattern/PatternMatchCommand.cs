using System.Collections.Generic;

public class PatternMatchCommand
{
    public PatternMatchResult GetTargetsFromSpot(TargetSpot targetSpot, Target targetType, PatternType patternType)
    {
        PatternMatchResult patternMatchResult = PatternMatchResult.Empty();
        switch (targetType)
        {
            case Target.Opponents: 
                patternMatchResult.CreaturesOpponent = patternType == PatternType.All ? 
                    AllInField() :
                    OpponentCreaturesInPattern(targetSpot.Position, patternType).ToArray();
                break;
            case Target.Frendly:
                patternMatchResult.CreaturesFriendly = patternType != PatternType.All ?
                    PlayerCreaturesInPattern(targetSpot.Position, patternType).ToArray() :
                    AllInField();
                break;
            case Target.Self:
                switch (targetSpot.SpotType)
                {
                    case SpotType.PlayerField: patternMatchResult.CreaturesFriendly = new int[1] { targetSpot.Position }; break;
                    case SpotType.OpponentField: patternMatchResult.CreaturesOpponent = new int[1] { targetSpot.Position };  break;
                    default: break;
                }
                break;
            case Target.All:
                patternMatchResult.CreaturesFriendly = AllInField();
                patternMatchResult.CreaturesOpponent = AllInField();
                break;
            case Target.None:
            default:
                break;
        }
        if (targetSpot.SpotType == SpotType.OpponentField) 
        {
            patternMatchResult = patternMatchResult.DoReverse();
        }
        return patternMatchResult;
    }

    private List<int> OpponentCreaturesInPattern(int startSpot, PatternType patternType)
    {
        List<int> select = new List<int>();
        PatternPosition[] positions = patternType.GetAsPositions();
        foreach (PatternPosition item in positions) 
        {
            int index = item.IndexDisplaceOppositeSide(startSpot);
            if(index != -1)
                select.Add(index);
        }
        return select;
    }

    private List<int> PlayerCreaturesInPattern(int startSpot, PatternType patternType)
    {
        List<int> select = new List<int>();
        PatternPosition[] positions = patternType.GetAsPositions();
        foreach (PatternPosition item in positions)
        {
            int index = item.IndexDisplaceSameSide(startSpot);
            if (index != -1)
                select.Add(index);
        }
        return select;
    }

    private int[] AllInField() => new int[6] { 0, 1, 2, 3, 4, 5 };
}

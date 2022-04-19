using System.Collections.Generic;
using System.Runtime.CompilerServices;
using p = DefaultPositions;

public static class DefaultPatters
{
    static DisciplesStyleFieldOperations DisciplesStyleFO = new DisciplesStyleFieldOperations();

    public static readonly PatternPosition[] LineInForward = new PatternPosition[2] { p.Front, p.FrontPlusOne };
    public static readonly PatternPosition[] ArcMelee = new PatternPosition[3] { p.Left, p.Front, p.Right };
    public static readonly PatternPosition[] LineLeft = new PatternPosition[2] { p.Left, p.LeftRanged };
    public static readonly PatternPosition[] LineRight = new PatternPosition[2] { p.Right, p.RightRanged };
    public static readonly PatternPosition[] ThreeLineMelee = new PatternPosition[3] { p.Front, p.Right, p.Left };
    public static readonly PatternPosition[] Neighbour = new PatternPosition[2] { p.LeftNeighbour, p.RightNeighbour };
    public static readonly PatternPosition[] Around = new PatternPosition[8]
    { p.Front, p.Left, p.Right, p.LeftNeighbour, p.RightNeighbour, p.Behind, p.LeftBehind, p.RightBehind};
    public static readonly PatternPosition[] None = new PatternPosition[0];
    //public static readonly PatternPosition[] All = new PatternPosition[6] { 0, 1, 2, 3, 4, 5};

    public static PatternPosition[] GetAsPositions(this PatternType patternType)
    {
        switch (patternType)
        {
            case PatternType.LineInForward:
                return LineInForward;
            case PatternType.ArcMelee:
                return ArcMelee;
            case PatternType.LineLeft:
                return LineLeft;
            case PatternType.LineRight:
                return LineRight;
            case PatternType.ThreeLineMelee:
                return ThreeLineMelee;
            case PatternType.SideNeighbour:
                return Neighbour;
            case PatternType.AroundOnYourSide:
                return Around;
            //case PatternType.
            case PatternType.All:
            default:
                return None;
        }
    }

    public static int[] GetAsIndexes(this PatternType patternType, int index)
    {
        if(patternType == PatternType.All || patternType == PatternType.AllYourField || patternType == PatternType.AllOponentsField)
            return new int[6] { 0, 1, 2, 3, 4, 5 };
        PatternPosition[] positions = patternType.GetAsPositions();
        return getIndexes(positions);
        
        int[] getIndexes(PatternPosition[] p) 
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < p.Length; i++) 
            {
                int indx = p[i].IndexDisplaceSameSide(index);
                if (indx != -1)
                    indexes.Add(indx);
            }
            return indexes.ToArray();
        }
    }


}

public class DefaultPositions
{
    // opposite side
    public static PatternPosition Front => new PatternPosition { x = 1, y = 0 };
    public static PatternPosition FrontPlusOne => new PatternPosition { x = 2, y = 0 };
    public static PatternPosition FrontPlusTwo => new PatternPosition { x = 3, y = 0 };

    public static PatternPosition Left => new PatternPosition { x = 1, y = -1 };
    public static PatternPosition LeftRanged => new PatternPosition { x = 2, y = -1 };
    public static PatternPosition Right => new PatternPosition { x = 1, y = 1 };
    public static PatternPosition RightRanged => new PatternPosition { x = 2, y = 1 };

    // frendly side
    public static PatternPosition ZeroSpot => new PatternPosition { x = 0, y = 0 };
    public static PatternPosition Behind => new PatternPosition { x = -1, y = 0 };
    public static PatternPosition LeftNeighbour => new PatternPosition { x = 0, y = -1 };
    public static PatternPosition LeftBehind => new PatternPosition { x = -1, y = -1 };
    public static PatternPosition RightNeighbour => new PatternPosition { x = 0, y = 1 };
    public static PatternPosition RightBehind => new PatternPosition { x = -1, y = 1 };
}
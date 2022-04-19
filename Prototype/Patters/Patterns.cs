public struct Pattern
{
    public PatternPosition[] PatternPositions;
}

public struct PatternPosition
{
    public int x;
    public int y;
}

public enum PatternType { 
    // opponent field
    LineInForward, ArcMelee, LineLeft, LineRight, ThreeLineMelee, AllOponentsField,
    // your field
    SideNeighbour, AroundOnYourSide, AllYourField,
    // both side
    All,
    // none
    None}
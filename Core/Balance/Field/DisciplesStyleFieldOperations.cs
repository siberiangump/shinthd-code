public class DisciplesStyleFieldOperations : IFieldOperations
{
    //     3 0 | 0 3    -y
    //     4 1 | 1 4 
    //  +x 5 2 | 2 5 -x +y

    public const int MAX_Y_INDEX = 2;
    public const int MIN_Y_INDEX = 0;

    public const int MAX_INDEX = 5;
    public const int MIN_INDEX = 0;
    
    public const int INVALID_INDEX = -1;

    public int IndexDisplaceOppositeSide(PatternPosition patternPosition, int index)
    {
        //int y = index + patternPosition.y;
        int result = index % 3 + patternPosition.y + (patternPosition.x - 1) * 3;
        return ValidateIndex(result);
    }
    
    public int IndexDisplaceSameSide(PatternPosition patternPosition, int index)
    {
        int y = index % 3 + patternPosition.y;
        if (ValidateYComponentIndex(y) == -1)
            return -1;
        int result = index + patternPosition.y + (patternPosition.x) * 3;
        return ValidateIndex(result);
    }

    public int[] GetMeleePriority(int spotIndex)
    {
        switch (spotIndex)
        {
            case 3: case 0: return new int[2] { 0, 3 };
            case 4: case 1: return new int[2] { 1, 4 };
            case 5: case 2: return new int[2] { 2, 5 };
            default: return new int[0];
        }
    }

    public int[] GetRangePriority(int spotIndex)
    {
        switch (spotIndex)
        {
            case 3: case 0: return new int[2] { 3, 0 };
            case 4: case 1: return new int[2] { 4, 1 };
            case 5: case 2: return new int[2] { 5, 2 };
            default: return new int[0];
        }
    }

    public int[] GetMeleeBlocker(int spotIndex)
    {
        switch (spotIndex)
        {
            case 3: return new int[1] { 0 };
            case 4: return new int[1] { 1 };
            case 5: return new int[1] { 2 };
            default: return new int[0];
        }
    }

    public int ValidateYComponentIndex(int yIndex) => yIndex > MAX_Y_INDEX || yIndex < MIN_Y_INDEX ? INVALID_INDEX : yIndex;

    public int ValidateIndex(int index) => index > MAX_INDEX || index < MIN_INDEX ? INVALID_INDEX : index;
}
public static class DisciplesStylePatternPositionExtention
{
    static DisciplesStyleFieldOperations disciplesStyleField = new DisciplesStyleFieldOperations();

    public static int IndexDisplaceOppositeSide(this PatternPosition patternPosition, int index)
    {
        return disciplesStyleField.IndexDisplaceOppositeSide(patternPosition, index); ;
    }

    public static int IndexDisplaceSameSide(this PatternPosition patternPosition, int index)
    {
        return disciplesStyleField.IndexDisplaceSameSide(patternPosition, index);
    }
}
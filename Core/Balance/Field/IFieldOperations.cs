public interface IFieldOperations
{
    int IndexDisplaceOppositeSide(PatternPosition patternPosition, int index);
    
    int IndexDisplaceSameSide(PatternPosition patternPosition, int index);
    
    int[] GetMeleePriority(int spotIndex);

    int[] GetMeleeBlocker(int spotIndex);
   
    int[] GetRangePriority(int spotIndex);
    
    int ValidateIndex(int index);
}

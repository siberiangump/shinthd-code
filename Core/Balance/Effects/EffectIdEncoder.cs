using System;

public static class EffectIdEncoder
{
    public static int GetBaseId(IEffect effect) 
    {
        switch (effect)
        {
            case BuffDamegeEffect buffDamegeEffect:
                break;
            case BuffHealthEffect buffHealthEffect:
                break;
            case DamageEffect damegeEffect:
                break;
            case HealEffect healEffect:
                break;
            case SpawnCreatureEffect spawnCreatureEffect:
                break;
            default:
                break;
        }
        return -1;
    }
}

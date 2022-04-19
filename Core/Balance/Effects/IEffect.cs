public interface IEffect { }

public static class IEffectExtention
{
    public static string ToString(this IEffect effect)
    {
        switch (effect)
        {
            case BuffDamegeEffect buffDamage: return $"BD:{buffDamage.DamegeAmount}";
            case BuffHealthEffect buffHealth: return $"BH:{buffHealth.HealthAmount}";
            case DamageEffect damage: return $"D:{damage.DamageAmount}";
            case HealEffect heal: return $"H:{heal.HealAmount}";
            case PoisonedEffect poisoned: return $"P:{poisoned.DamageAmount}|T:{poisoned.ForTurns}";
            case GivePoisonedEffect givePoisoned: return $"GP:{givePoisoned.DamageAmount}|T:{givePoisoned.ForTurns}";
            case SpawnCreatureEffect spawnCreature: return $"S|ID:{spawnCreature.CreatureId}";
            default: return "none";
        }
    }

    public static string GetCodeName(this IEffect effect) => effect switch
    {
        BuffDamegeEffect _ => "bd",
        BuffHealthEffect _ => "bh",
        DamageEffect _ => "d",
        HealEffect _ => "h",
        PoisonedEffect _ => "p",
        GivePoisonedEffect _ => "gp",
        SpawnCreatureEffect _ => "s",
        _ => "none"
    };

    public static string GetSpriteName(this IEffect effect) => effect.GetSpriteName();
}

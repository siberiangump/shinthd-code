using System.Linq;

public static class EffectBalanceModelOperations
{
    public static BuffDamegeEffect GetBuffDamage(this EffectBalanceModel model, int id) => model.BuffDamege.FirstOrDefault(x => x.Id == id);
    public static BuffHealthEffect GetBuffHealth(this EffectBalanceModel model, int id) => model.BuffHealth.FirstOrDefault(x => x.Id == id);
    public static HealEffect GetHeal(this EffectBalanceModel model, int id) => model.Heal.FirstOrDefault(x => x.Id == id);
    public static DamageEffect GetDamage(this EffectBalanceModel model, int id) => model.Damage.FirstOrDefault(x => x.Id == id);
    public static PoisonedEffect GetPoisoned(this EffectBalanceModel model, int id) => model.Poisoned.FirstOrDefault(x => x.Id == id);
    public static GivePoisonedEffect GetGivePoisoned(this EffectBalanceModel model, int id) => model.GivePoisoned.FirstOrDefault(x => x.Id == id);
    public static SpawnCreatureEffect GetSpawnCreature(this EffectBalanceModel model, int id) => model.SpawnCreature.FirstOrDefault(x => x.Id == id);
    public static SleepEffect GetSleep(this EffectBalanceModel model, int id) => model.Sleep.FirstOrDefault(x => x.Id == id);

    public static IEffect GetEffect(this EffectBalanceModel model, EffectItem item) 
    {
        switch (item.Effect)
        {
            case EffectType.BuffDamage: return model.GetBuffDamage(item.EffectId);
            case EffectType.BuffHealth: return model.GetBuffHealth(item.EffectId);
            case EffectType.Damage: return model.GetDamage(item.EffectId);
            case EffectType.Heal: return model.GetHeal(item.EffectId);
            case EffectType.Poisoned: return model.GetPoisoned(item.EffectId);
            case EffectType.GivePoisoned: return model.GetGivePoisoned(item.EffectId);
            case EffectType.SpawnCreature: return model.GetSpawnCreature(item.EffectId);
            case EffectType.Sleep: return model.GetSleep(item.EffectId);
            case EffectType.None:
            default: return null;
        }
    }
}

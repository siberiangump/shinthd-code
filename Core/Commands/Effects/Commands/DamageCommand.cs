using UnityEngine;

public class DamageCommand
{
    private void DoOnCreature(DamageEffect effect, int buff, Character target) 
    {
            target.TakenDamage += effect.DamageAmount + buff;
            if (target.TakenDamage >= target.Health)
                target.Dead = true;
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnCreatures(DamageEffect effect, Character from, Character[] targets, BalanceModel cardBalance)
    {
        for (int i = 0; i < targets.Length; i++)
            DoOnCreature(effect, GetDamageBuffAmount(from, cardBalance.Effects), targets[i]);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnHero(DamageEffect effect, Character from, Character target, BalanceModel cardBalance)
    {
        GetDamageBuffAmount(from, cardBalance.Effects);
        target.TakenDamage += effect.DamageAmount + GetDamageBuffAmount(from, cardBalance.Effects);
    }

    public int GetDamageBuffAmount(Character fieldCreatureModel, EffectBalanceModel effectBalanceModel) 
    {
        int result = 0;
        for (int i = 0; i < fieldCreatureModel.Effects.Count; i++)
        {
            EffectStatus item = fieldCreatureModel.Effects[i];
            if (item.Type == EffectType.BuffDamage) 
            {
                BuffDamegeEffect buffDamege = effectBalanceModel.GetBuffDamage(item.Id);
                result += buffDamege.DamegeAmount;
            }
        }
        return result;
    }
}

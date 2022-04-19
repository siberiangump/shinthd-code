using System;
using UnityEngine;

public class BuffDamageCommand
{
    public void DoOnCreature(BuffDamegeEffect effect, Character target)
    {
        int effectIndex = GetSameEffectIndex(effect, target);
        if (effectIndex != -1)
            target.Effects[effectIndex].ForTurns = effect.TurnAmount;
        else
            target.Effects.Add(new EffectStatus { Id = effect.Id, ForTurns = effect.TurnAmount, Type = EffectType.BuffDamage });
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnCreatures(BuffDamegeEffect effect, Character[] targets)
    {
        for (int i = 0; i < targets.Length; i++)
            DoOnCreature(effect, targets[i]);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnHero(BuffDamegeEffect effect, Character target)
    {
        //target.TakenDamage = Mathf.Clamp(target.TakenDamage - effect.HealAmount, 0, int.MaxValue);
    }

    private int GetSameEffectIndex(BuffDamegeEffect effect, Character target)
    {
        for (int i = 0; i < target.Effects.Count; i++)
            if (target.Effects[i].Type == EffectType.BuffDamage && effect.Id == target.Effects[i].Id)
                return i;
        return -1;
    }
}

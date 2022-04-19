using System;
using UnityEngine;

public class GivePoisonedCommand
{
    public void DoOnCreature(GivePoisonedEffect effect, Character target)
    {
        //target.TakenDamage = Mathf.Clamp(target.TakenDamage - effect.HealAmount, 0, int.MaxValue);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnCreatures(GivePoisonedEffect effect, Character[] targets)
    {
        //for (int i = 0; i < targets.Length; i++)
        //    DoOnCreature(effect, targets[i]);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnHero(GivePoisonedEffect effect, Character target)
    {
        //target.TakenDamage = Mathf.Clamp(target.TakenDamage - effect.HealAmount, 0, int.MaxValue);
    }
}

using System;
using UnityEngine;

public class PoisonedCommand
{
    public void DoOnCreature(PoisonedEffect effect, Character target)
    {
        //target.TakenDamage = Mathf.Clamp(target.TakenDamage - effect.HealAmount, 0, int.MaxValue);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnCreatures(PoisonedEffect effect, Character[] targets)
    {
        //for (int i = 0; i < targets.Length; i++)
        //    DoOnCreature(effect, targets[i]);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnHero(PoisonedEffect effect, Character target)
    {
        //target.TakenDamage = Mathf.Clamp(target.TakenDamage - effect.HealAmount, 0, int.MaxValue);
    }
}

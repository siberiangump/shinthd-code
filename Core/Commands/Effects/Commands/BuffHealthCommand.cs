using System;
using UnityEngine;

public class BuffHealthCommand
{
    public void DoOnCreature(BuffHealthEffect effect, Character target)
    {
        //target.TakenDamage = Mathf.Clamp(target.TakenDamage - effect.HealAmount, 0, int.MaxValue);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnCreatures(BuffHealthEffect effect, Character[] targets)
    {
        //for (int i = 0; i < targets.Length; i++)
        //    DoOnCreature(effect, targets[i]);
    }

    /// <summary>
    /// no null check
    /// </summary>
    public void DoOnHero(BuffHealthEffect effect, Character target)
    {
        //target.TakenDamage = Mathf.Clamp(target.TakenDamage - effect.HealAmount, 0, int.MaxValue);
    }
}

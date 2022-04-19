using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

[Obsolete("i not sure how to use this for now")]
public interface IDamageable { }

public static class IDamageableOperations 
{
    public static int GetTakenDamage(this IDamageable target) 
    {
        switch (target)
        {
            case Character hero: return hero.TakenDamage;
            default: return -1;
        }
    }
    
    public static int AddTakenDamage(this IDamageable target,int amount)
    {
        switch (target)
        {
            case Character hero: hero.TakenDamage += amount; return hero.TakenDamage;
            default: return -1;
        }
    }

    public static bool IsDead(this IDamageable target) 
    {
        switch (target)
        {
            case Character hero: return hero.Dead;
            default: return false;
        }
    }

    public static void SetDead(this IDamageable target, bool value)
    {
        switch (target)
        {
            case Character hero: hero.Dead = value; break;
            default: break;
        }
    }
}
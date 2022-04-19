
using System;
using System.Collections.Generic;

public class AggregatedEffectCommand
{
    private DamageCommand DamageCommand = new DamageCommand();
    private HealCommand HealCommand = new HealCommand();
    private BuffDamageCommand BuffDamageCommand = new BuffDamageCommand();
    private BuffHealthCommand BuffHealthCommand = new BuffHealthCommand();
    private PoisonedCommand PoisonedCommand = new PoisonedCommand();
    private GivePoisonedCommand GivePoisonedCommand = new GivePoisonedCommand();

    public FightState DoEffect(Character fromCreature, EffectItem effectItem, IEffect effect,PatternMatchResult patternMatch, FightState fight, BalanceModel cardBalance)
    {
        Targets targets = patternMatch.SelectCreatures(fight);
        switch (effectItem.Effect)
        {
            case EffectType.BuffDamage: DoBuffDamage(targets, fromCreature, (BuffDamegeEffect)effect); break;
            case EffectType.BuffHealth: DoBuffHealth(targets, fromCreature, (BuffHealthEffect)effect); break;
            case EffectType.Damage: DoDamage(targets, fromCreature, (DamageEffect)effect, cardBalance); break;
            case EffectType.Heal: DoHeal(targets, fromCreature, (HealEffect)effect); break;
            case EffectType.Poisoned: DoPoisoned(targets, fromCreature, (PoisonedEffect)effect); break;
            case EffectType.GivePoisoned: GivePoisoned(targets, fromCreature, (GivePoisonedEffect)effect); break;
            case EffectType.SpawnCreature:
                break;
            case EffectType.None:
                break;
            default:
                break;
        }
        return fight;
    }

    private void DoDamage(Targets targets, Character fromCreature,DamageEffect damage, BalanceModel cardBalance)
    {
        if (targets.Creatures.Length > 0) DamageCommand.DoOnCreatures(damage, fromCreature, targets.Creatures, cardBalance);
    }

    private void DoHeal(Targets targets, Character fromCreature, HealEffect heal)
    {
        if (targets.Creatures.Length > 0) HealCommand.DoOnCreatures(heal, targets.Creatures);
    }

    private void DoBuffDamage(Targets targets, Character fromCreature, BuffDamegeEffect buff)
    {
        if (targets.Creatures.Length > 0) BuffDamageCommand.DoOnCreatures(buff, targets.Creatures);
    }

    private void DoBuffHealth(Targets targets, Character fromCreature, BuffHealthEffect buff)
    {
        if (targets.Creatures.Length > 0) BuffHealthCommand.DoOnCreatures(buff, targets.Creatures);
    }

    private void DoPoisoned(Targets targets, Character fromCreature, PoisonedEffect poisoned)
    {
        if (targets.Creatures.Length > 0) PoisonedCommand.DoOnCreatures(poisoned, targets.Creatures);
    }

    private void GivePoisoned(Targets targets, Character fromCreature, GivePoisonedEffect poisoned)
    {
        if (targets.Creatures.Length > 0) GivePoisonedCommand.DoOnCreatures(poisoned, targets.Creatures);
    }
}
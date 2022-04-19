public interface ICreatureModel {}


public static class ICreatureModelExtantion
{
    public static int CreatureCardId(this ICreatureModel card)
    {
        switch (card)
        {
            case CreatureModel creature:
                return creature.id;
            default:
                return 0;
        }
    }

    public static int Attack(this ICreatureModel card)
    {
        switch (card)
        {
            case CreatureModel creature:
                return creature.Attack;
            default:
                return 0;
        }
    }

    public static int Health(this ICreatureModel card)
    {
        switch (card)
        {
            case CreatureModel creature:
                return creature.Live;
            default:
                return 0;
        }
    }

    public static int Cost(this ICreatureModel card)
    {
        switch (card)
        {
            case CreatureModel creature:
                return creature.Cost;
            default:
                return 0;
        }
    }

    public static string Name(this ICreatureModel card)
    {
        switch (card)
        {
            case CreatureModel creature:
                return creature.Name;
            default:
                return string.Empty;
        }
    }

    public static string Text(this ICreatureModel card)
    {
        switch (card)
        {
            case CreatureModel creature:
                return creature.Text;
            default:
                return string.Empty;
        }
    }

    public static EffectItem Action(this ICreatureModel card)
    {
        switch (card)
        {
            case CreatureModel creature:
                return creature.Action;
            default:
                return EffectItem.Empty();
        }
    }
}


using System.Collections.Generic;

[System.Serializable]
public class Party
{
    public Character[] Characters;
    public BattleFieldModel Field;
    public int ActionPoints;
}

[System.Serializable]
public class Character
{
    public CharacterId Type;
    public int Health;
    public bool Dead;
    public int TakenDamage;
    public List<SkillId> Skills; 
    public List<EffectStatus> Effects;
}

[System.Serializable]
public class BattleFieldModel
{
    public int[] Spot;
}

static class ModelConstants
{
    public const int Empty = -1;
}


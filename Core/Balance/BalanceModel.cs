using System.Collections.Generic;

[System.Serializable]
public class BalanceModel
{
    public SkillsBalance Skills;
    public EffectBalanceModel Effects;
}

public class SkillsBalance 
{
    public Move Move;
    public PistolShoot PistolShoot;
}
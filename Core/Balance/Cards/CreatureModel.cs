[System.Serializable]
public class CreatureModel : INameOwner, IActionModel, ICreatureModel
{
    public string Name;
    public int id = -1;
    public int Cost;
    public int Attack;
    public int Live;
    public string Text;

    public EffectItem Action;
    //public EffectItem[] Effects;
}

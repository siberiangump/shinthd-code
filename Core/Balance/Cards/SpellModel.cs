[System.Serializable]
public class SpellModel : INameOwner, IActionModel
{
    public string Name;
    public int id = -1;
    public int Cost;
    public string Text;
}

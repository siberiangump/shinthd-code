//public class Skill
//{
//	public int Id;
//	public int AP;
//	public int CD;
//	public int ChargeTime;
//}

public interface ISkill { }

public struct Move
{
	public int Id;
	public int AP;
	public Pattern Pattern;
}

public struct PistolShoot
{
	public int Id;
	public int AP;
	public TargetSelectionType TargetSelectionType; // TargetSelectionType.Ranger
	public Damage.Dice Damage; // d6
	public int CritChance; // 2 
}

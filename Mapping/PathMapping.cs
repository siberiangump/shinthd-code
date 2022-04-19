using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathMapping
{
	private static string BaseAnamationPath(CharacterId id) => $"Assets/Assets/Animation/Characters/{Architype(id)}";
	private static string BasePrefabPath(CharacterId id) => $"Assets/Assets/Prefabs/Characters/{Architype(id)}";

	public static Dictionary<CharacterId, string> CharacterName = new Dictionary<CharacterId, string>
	{
		{CharacterId.Test , CharacterId.Test.ToString() },
		// Hero
		{CharacterId.Rogue , CharacterId.Rogue.ToString()  },
		{CharacterId.Cleric , CharacterId.Cleric.ToString() },
		{CharacterId.Mage , CharacterId.Mage.ToString() },
		{CharacterId.Warrior , CharacterId.Warrior.ToString() },
		// Enemy
		{CharacterId.Risen , CharacterId.Risen.ToString() },
		{CharacterId.Lich , CharacterId.Lich.ToString() }
	};

	public static Dictionary<SkillId, string> SkillName = new Dictionary<SkillId, string>
	{
		// common 0000
		{ SkillId.Move, SkillId.Move.ToString() },
		{ SkillId.Idle, SkillId.Idle.ToString() },
		{ SkillId.Dodge, SkillId.Dodge.ToString() },
		{ SkillId.GetDamage, SkillId.GetDamage.ToString() },
		{ SkillId.Dead, SkillId.Dead.ToString() },
		{ SkillId.Charge, SkillId.Charge.ToString() },
		{ SkillId.IdleCharge, SkillId.IdleCharge.ToString() },

		// Rogue  1000
		{ SkillId.PistolShoot, SkillId.PistolShoot.ToString() },
		{ SkillId.Mockery, SkillId.Mockery.ToString() },
		
		// Cleric 1100
		{ SkillId.VampireClaws, SkillId.VampireClaws.ToString() },
		{ SkillId.VampireHealing, SkillId.VampireHealing.ToString() },
		
		// Warrior 1200
		{ SkillId.HammerStrike, SkillId.HammerStrike.ToString() },
		{ SkillId.HammerSwing, SkillId.HammerSwing.ToString() },
		
		// Mage 1300
		{ SkillId.FlameBreath, SkillId.FlameBreath.ToString() },
		{ SkillId.LavaFloor, SkillId.LavaFloor.ToString() },
		
		// Zombi 2000
		{ SkillId.ZombiStrike, SkillId.ZombiStrike.ToString() },

		// Lich 2100
		{ SkillId.RizeDead, SkillId.RizeDead.ToString() },
		{ SkillId.BuffDead, SkillId.BuffDead.ToString() },
		{ SkillId.AttackMurena, SkillId.AttackMurena.ToString() },
		{ SkillId.AttackMurenaEnhance, SkillId.AttackMurenaEnhance.ToString() },
		{ SkillId.ConsumeDead, SkillId.ConsumeDead.ToString() },
		{ SkillId.Resurect, SkillId.Resurect.ToString() },
	};

	private static string Architype(CharacterId id)
	{
		int idint = (int)id;
		int numb = idint / 10;
		if (numb == 1)
			return "Hero";
		if (numb == 2)
			return "Enemy";
		return "Error";
	}


	public static string GetAnimationFolderPath(CharacterId id) => $"{BaseAnamationPath(id)}/{CharacterName[id]}";

	public static string GetAnimationControllerPath(CharacterId id) => $"{GetAnimationFolderPath(id)}/{CharacterName[id]}.controller";

	public static string GetAnimationPath(CharacterId id, string animation) => $"{GetAnimationFolderPath(id)}/{CharacterName[id]}_{animation}.anim";

	public static string GetPrefabPath(CharacterId id) => $"{BasePrefabPath(id)}/{CharacterName[id]}.prefab";
}
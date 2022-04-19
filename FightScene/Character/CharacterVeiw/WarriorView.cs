using UnityEngine;
using System.Collections;
using System;

public class WarriorView : CharacterView
{
	[SerializeField] HammerStrikeIntent HammerStrikeIntent;

	public override SkillId[] GetSkills()
	{
		return new SkillId[3] { SkillId.Move, SkillId.HammerStrike, SkillId.HammerSwing };
	}

	protected override void ExecuteCharecterSkill(SkillId skillId)
	{
		switch (skillId)
		{
			case SkillId.HammerStrike: HammerStrikeIntent.SelectAndDo(this, ExecuteSkillEnd); break;
			case SkillId.HammerSwing: ExecuteSkillEnd(); break;
			default: break;
		}
	}
}

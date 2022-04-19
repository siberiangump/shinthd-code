using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageView : CharacterView
{
    [SerializeField] FlameBreathIntent FlameBreathIntent;
    [SerializeField] CharacterView[] Targets;

	public override SkillId[] GetSkills()
	{
        return new SkillId[] { SkillId.Move, SkillId.FlameBreath, SkillId.LavaFloor };
	}

	protected override void ExecuteCharecterSkill(SkillId skillId)
	{
		switch (skillId)
		{
			case SkillId.FlameBreath: FlameBreathIntent.ChooseTarget(this, ExecuteSkillEnd); break;
			case SkillId.LavaFloor: break;
			default:
				break;
		}
	}
}

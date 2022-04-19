using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClericView : CharacterView
{
	[SerializeField] VampireClawsIntent VampireClawsIntent;

	public override SkillId[] GetSkills() 
	{
		return new SkillId[] { SkillId.Move, SkillId.VampireClaws, SkillId.VampireHealing };
	}

	protected override void ExecuteCharecterSkill(SkillId skillId)
	{
		switch (skillId)
		{
			case SkillId.None:
				break;
			case SkillId.VampireClaws:
				break;
			case SkillId.VampireHealing:
				break;
			default:
				break;
		}
	}
}

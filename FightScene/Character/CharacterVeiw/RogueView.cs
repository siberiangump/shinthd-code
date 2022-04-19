using UnityEngine;
using System.Collections;

public class RogueView : CharacterView
{
    [SerializeField] PistolShootIntent PistolShootIntent;

	public override SkillId[] GetSkills()
	{
		return new SkillId[] {SkillId.Move, SkillId.PistolShoot, SkillId.Mockery };
	}

	protected override void ExecuteCharecterSkill(SkillId skillId)
	{
		switch (skillId)
		{
			case SkillId.PistolShoot: PistolShootIntent.ChooseTarget(this, ExecuteSkillEnd); break;
			case SkillId.Mockery: break;
			default: break;
		}
	}

	[ContextMenu("TestShoot")]
    public void TestShoot()
    {
        PistolShootIntent.ChooseTarget(this, ExecuteSkillEnd);
    }
}

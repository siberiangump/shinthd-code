using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillButton : MonoBehaviour, ISelectIntentTarget
{
	[SerializeField] SkillId SkillId;
	[SerializeField] SpriteRenderer Renderer;
	[SerializeField] Collider Collider;

	SkillPresetData SkillPreset;

	public void Init(SkillPresetData skillPreset) 
	{
		SkillPreset = skillPreset;
	}

	public SkillId GetSkillId() 
	{
		return SkillId;
	}

	public void SetSkill(SkillId skillId) 
	{
		SkillId = skillId;
		Renderer.sprite = SkillPreset.GetSpriteBySkill(SkillId);
		this.gameObject.SetActive(skillId != SkillId.None);
	}

	SelectData ISelectIntentTarget.GetSelectData()
	{
		return new SelectData { Collider = Collider, Target = this };
	}
}

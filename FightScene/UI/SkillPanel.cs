using System;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
	[SerializeField] SelectIntent SelectIntent;
	[SerializeField] SkillPresetData SkillPreset;

	[SerializeField] SkillButton[] Skills;
	[SerializeField] Animator Animator;

	private void Awake()
	{
		for (int i = 0; i < Skills.Length; i++)
			Skills[i].Init(SkillPreset);
	}

	public void SelectSkill(SkillId[] skills, Action<SkillId> onSelect) 
	{
		Animator.Play("Show");
		SetUpSkills(skills);
		SelectIntent.SelectOne(Skills, OnHover, OnStopHover, (x) => onSelect(OnSelect(x)), true);
	}

	private void OnHover(int index)
	{
	}

	private void OnStopHover(int index)
	{
	}

	private SkillId OnSelect(int index)
	{
		Animator.Play("Hide");
		if (index == -1)
			return SkillId.None;
		return Skills[index].GetSkillId();
	}

	private void SetUpSkills(SkillId[] skills) 
	{
		for (int i = 0; i < skills.Length; i++)
			Skills[i].SetSkill(skills[i]);
	}
}

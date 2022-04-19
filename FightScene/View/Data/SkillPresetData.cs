using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "SkillPresetData", menuName = "ScriptableObjects/Presets/SkillPreset", order = 1000)]
public class SkillPresetData : ScriptableObject
{
	[SerializeField] internal SkillData[] Skills;
}

[System.Serializable]
public struct SkillData 
{
	public SkillId SkillId;
	public Sprite Icon;
}

public static class SkillPresetDataOpperations 
{
	public static Sprite GetSpriteBySkill(this SkillPresetData skillPreset, SkillId skillId)
	{
		return skillPreset.Skills.FirstOrDefault((x) => x.SkillId == skillId).Icon;
	}
}
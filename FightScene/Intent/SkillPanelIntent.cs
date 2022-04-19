using System;
using UnityEngine;

public class SkillPanelIntent : MonoBehaviour
{
	[SerializeField] SkillPanel StrictPanel;
	[SerializeField] SkillPanel ReversePanel;

    public void ShowSkillPanel(CharacterView character, CharacterViewPreset viewPreset, Action<SkillId> onSelectSkill)
    {
        SkillPanel skillPanel = character.GetCharacterRotation() == CharacterRotation.Strict ? StrictPanel : ReversePanel;
        skillPanel.transform.localPosition = viewPreset.PanelPosition.Position[character.GetSpotIndex()];
        skillPanel.SelectSkill(character.GetSkills(), onSelectSkill);
    }

#if UNITY_EDITOR
    public void StorePanelPosition(CharacterView character, CharacterViewPreset viewPreset)
    {
        int index = character.GetSpotIndex();
        viewPreset.PanelPosition.Position[index] = character.GetCharacterRotation() == CharacterRotation.Strict ? StrictPanel.transform.localPosition : ReversePanel.transform.localPosition;
    }
#endif
}

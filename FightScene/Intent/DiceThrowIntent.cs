using System;
using UnityEngine;

public class DiceThrowIntent : MonoBehaviour
{
    [SerializeField] DicePanel StrictPanel;
    [SerializeField] DicePanel ReversePanel;

    public void ShowDicePanel(CharacterView character, Action onEnd)
    {
        DicePanel skillPanel = character.GetCharacterRotation() == CharacterRotation.Strict ? StrictPanel : ReversePanel;
        skillPanel.transform.localPosition = character.ViewPreset.PanelPosition.Position[character.GetSpotIndex()];
        skillPanel.Throw(onEnd);
    }

#if UNITY_EDITOR
    public void StorePanelPosition(CharacterView character, CharacterViewPreset viewPreset)
    {
        int index = character.GetSpotIndex();
        viewPreset.PanelPosition.Position[index] = character.GetCharacterRotation() == CharacterRotation.Strict ? StrictPanel.transform.localPosition : ReversePanel.transform.localPosition;
    }
#endif
}

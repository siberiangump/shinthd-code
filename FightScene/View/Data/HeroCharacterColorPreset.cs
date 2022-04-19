using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroCharacterColorPresetData", menuName = "ScriptableObjects/Presets/HeroCharacterColor", order = 1000)]
public class HeroCharacterColorPreset : ScriptableObject
{
    public Color Default;
    public Color Selectable;
    public Color NonSelectable;
    public Color Selected;
    public Color NotSelected;
    public Color NotSelectedOverlap;
}

public static class HeroCharacterColorPresetDataOpperations
{
    public static Color GetColor(this HeroCharacterColorPreset preset, ViewStateEnum state)
    {
        switch (state)
        {
            case ViewStateEnum.Selectable: return preset.Selectable;
            case ViewStateEnum.NotSelectable: return preset.NonSelectable;
            case ViewStateEnum.Hover: return preset.Selected;
            case ViewStateEnum.NotHover: return preset.NotSelected;
            case ViewStateEnum.NotHoverOverlap: return preset.NotSelectedOverlap;
            case ViewStateEnum.Default:
            default:
                return preset.Default;
        }
    }
}

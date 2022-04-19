using UnityEngine;

[CreateAssetMenu(fileName = "MaterialPresetData", menuName = "ScriptableObjects/Presets/Material", order = 1000)]
public class MaterialPresetData : ScriptableObject
{
    public Material Default;
    public Material Selectable;
    public Material NonSelectable;
    public Material Selected;
}

public static class MaterialPresetDataOpperations
{
    public static Material GetMaterial(this MaterialPresetData materialPreset, ViewStateEnum state)
    {
        switch (state)
        {
            case ViewStateEnum.Selectable: return materialPreset.Selectable;
            case ViewStateEnum.NotSelectable: return materialPreset.NonSelectable;
            case ViewStateEnum.Hover: return materialPreset.Selected;
            case ViewStateEnum.Default: 
            default:
                return materialPreset.Default;
        }
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterViewPreset", menuName = "ScriptableObjects/Presets/CharacterViewPreset", order = 1000)]
public class CharacterViewPreset : ScriptableObject
{
    public CharacterId Character;
    public CharacterRotation BaseRotation;
    public PanelPosition PanelPosition = new PanelPosition { Position = new Vector3[6] } ;
    public CharacterPosition CharacterPosition = new CharacterPosition { Position = new Vector3[6] };
    public EffectPosition EffectPosition = new EffectPosition { Position = new Vector3[6] };
}

[System.Serializable]
public struct PanelPosition 
{
    public Vector3[] Position;
}

[System.Serializable]
public struct CharacterPosition
{
    public Vector3[] Position;
}

[System.Serializable]
public struct EffectPosition
{
    public Vector3[] Position;
}

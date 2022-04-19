using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour , ICharacter
{
    [SerializeField] SpriteRenderer Renderer;
    [SerializeField] HeroCharacterColorPreset HeroCharacterColorPreset;

    public void SetState(ViewStateEnum state)
    {
        Renderer.color = HeroCharacterColorPreset.GetColor(state);
    }
}
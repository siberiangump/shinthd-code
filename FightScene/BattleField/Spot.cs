using System;
using UnityEngine;

public class Spot : MonoBehaviour, ISelectIntentTarget
{
    [SerializeField] Indicator Indicator;
    [SerializeField] Collider Collider;
    [SerializeField] Vector2Int Position;
    [SerializeField] CharacterRotation CharacterRotation;

    [Header("Dynamic")]
    [ValidateField(typeof(ICharacter))] public MonoBehaviour Character;

    private ViewStateEnum BaseState;
    private bool Locked = false;

    public void Init()
    {
        BaseState = ViewStateEnum.Selectable;
        SetState(ViewStateEnum.Selectable);
    }

    public CharacterRotation GetCharacterRotation() => CharacterRotation;

    public ViewStateEnum GetBaseState() => BaseState;
    
    public void SetBaseState(ViewStateEnum state, bool lockState = false)
    {
        BaseState = state;
        bool selectable = BaseState == ViewStateEnum.Selectable;
        Collider.enabled = selectable && !lockState;
        if(lockState)
            Indicator.SetState(ViewStateEnum.Default);
        else 
            Indicator.SetState(state);

        SetState(state);
        Locked = lockState;
    }

    public void DisableLock() 
    {
        Locked = false;
    }

    //TEMP
    public bool HaveCharacter()
    {
        if (Character == null)
            Character = this.GetComponentInChildren<CharacterView>();
        return Character != null;
    }

    public void SetState(ViewStateEnum state, bool force = false)
    {
        if (Locked && !force)
            return;
        if (BaseState == ViewStateEnum.Selectable)
            Indicator.SetState(state);
        (Character as ICharacter)?.SetState(state);
    }

    public void ResetState(bool force = false)
    {
        if (Locked && !force)
            return;
        SetState(BaseState, force);
    }

    public CharacterView GetCheracter() 
    {
        if (Character == null)
            Character = this.GetComponentInChildren<CharacterView>();
        return Character as CharacterView;
    }

    public void SetCheracter(CharacterView characterView)
    {
        Character = characterView;
    }

    SelectData ISelectIntentTarget.GetSelectData() => new SelectData { Target = this, Collider = Collider };
}

public enum CharacterRotation { Strict = 1 , Reverse = -1 }
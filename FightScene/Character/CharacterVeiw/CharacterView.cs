using System;
using UnityEngine;

public class CharacterView : MonoBehaviour, ICharacter
{
	[Header("Static Refferrences")]
	[SerializeField] internal CharacterId Character;
	[SerializeField] internal CharacterAnimation CharacterAnimator;

	[SerializeField] internal SpriteRenderer Renderer;
	[SerializeField] internal HeroCharacterColorPreset ColorPresetData;
	[SerializeField] internal CharacterViewPreset ViewPreset;

	[Header("Dynamic Refferrences")]
	[SerializeField] SkillPanelIntent SkillPanelIntent;

	CallbackHolder OnExecuteSkillEnd = new CallbackHolder();

	private void Awake()
	{
		CorrectPositionAndRotation();
	}

	[ContextMenu("CorrectPositionAndRotation")]
	public void CorrectPositionAndRotation()
	{
		CorrectPosition();
		SetRotation(GetCharacterRotation());
		GetComponent<PixelPerfectCorrectorSpriteRenderer>()?.Correct();
	}

	public void SetRotation(CharacterRotation rotation)
	{
		Renderer.flipX = (CharacterRotation)((int)rotation * (int)ViewPreset.BaseRotation) == CharacterRotation.Strict ? false : true;
	}

	public void SetState(ViewStateEnum state)
	{
		Renderer.color = ColorPresetData.GetColor(state);
	}

	public void AnimateCharacter(SkillId skillId, Action onEnd)
	{
		CharacterAnimator.PlayAnimation(Character, skillId, onEnd);
	}

	public void CorrectPosition()
	{
		int index = GetSpotIndex();
		this.transform.localPosition = ViewPreset.CharacterPosition.Position[index];
	}

	public void Action(Action onEnd)
	{
		OnExecuteSkillEnd.SetCallback(onEnd);
		SkillPanelIntent.ShowSkillPanel(this, ViewPreset, ExecuteSkill);
	}

	#region skills

	[SerializeField] MoveIntent MoveIntent;

	private void ExecuteSkill(SkillId skillId)
	{
		switch (skillId)
		{
			// common skills
			case SkillId.Move: Move(ExecuteSkillEnd); break;
			// cancel
			case SkillId.None: ExecuteSkillEnd(); break;
			// character skills
			default: ExecuteCharecterSkill(skillId); break;
		}
	}

	protected void ExecuteSkillEnd()
	{
		OnExecuteSkillEnd.Invoke();
	}

	public void Move(Action onEnd)
	{
		MoveIntent.SelectAndMove(this, onEnd);
	}
	#endregion

	#region Virtual
	protected virtual void ExecuteCharecterSkill(SkillId skillId) { }

	public virtual SkillId[] GetSkills() { return new SkillId[3] { SkillId.None, SkillId.None, SkillId.None }; }
	#endregion

	#region KILL this SHit
	public int GetSpotIndex()
	{
		Spot spot = this.transform.parent.GetComponent<Spot>();
		Field field = spot.transform.parent.GetComponent<Field>();
		return field.GetSpotIndex(spot);
	}

	public CharacterRotation GetCharacterRotation()
	{
		Spot spot = this.transform.parent.GetComponent<Spot>();
		return spot.GetCharacterRotation();
	}

	public FieldSide GetFieldSide()
	{
		Spot spot = this.transform.parent.GetComponent<Spot>();
		Field field = spot.transform.parent.GetComponent<Field>();
		return field.GetSide();
	}
	#endregion

#if UNITY_EDITOR
	[ContextMenu("StorePosition")]
	public void StorePosition()
	{
		int index = GetSpotIndex();
		ViewPreset.CharacterPosition.Position[index] = this.transform.localPosition;
	}
#endif
}

public static class CharacterViewGetters
{
	public static Vector3 Get_PositionCorrection(this CharacterView c, int position) => c.ViewPreset.CharacterPosition.Position[position];
	
	public static int Get_SpotIndex(this CharacterView c)
	{
		Spot spot = c.transform.parent.GetComponent<Spot>();
		Field field = spot.transform.parent.GetComponent<Field>();
		return field.GetSpotIndex(spot);
	}

	public static CharacterRotation Get_CharacterRotation(this CharacterView c)
	{
		Spot spot = c.transform.parent.GetComponent<Spot>();
		return spot.GetCharacterRotation();
	}

	public static FieldSide Get_FieldSide(this CharacterView c)
	{
		Spot spot = c.transform.parent.GetComponent<Spot>();
		Field field = spot.transform.parent.GetComponent<Field>();
		return field.GetSide();
	}
}
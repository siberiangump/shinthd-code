using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIntent : MonoBehaviour
{
	[SerializeField] private PatternType Pattern;

	[SerializeField] FieldSelectIntent SelectIntent;
	[SerializeField] Field HeroField;
	[SerializeField] Field EnemyField;

	public void SelectAndMove(CharacterView character, Action onEnd)
	{
		int startPosition = character.GetSpotIndex();
		FieldSide fieldSide = character.GetFieldSide();
		Field field = fieldSide == FieldSide.Hero ? HeroField : EnemyField;
		SelectIntent.SelectOne(
			field,
			startPosition,
			Pattern,
			FieldSelectionType.Any,
			(selectedIndex) => Move(character, selectedIndex, field.GetSpot(selectedIndex), startPosition, field.GetSpot(startPosition), onEnd),
			GetLockStateData(fieldSide, startPosition)
		);
	}

	public void Move(CharacterView characte, int position, Spot spot, int startPosition, Spot startSpot, Action end)
	{
		StartCoroutine(MoveCharacter(characte, position, spot, startPosition, startSpot, end));
	}

	IEnumerator MoveCharacter(CharacterView characte, int position, Spot spot, int startPosition, Spot startSpot, Action end)
	{

		yield return new WaitForEndOfFrame();

		CharacterView swapCharacter = spot.GetCheracter();
		CharacterStartMove(characte, position, spot);
		spot.SetCheracter(characte);
		startSpot.SetCheracter(swapCharacter);

		if (swapCharacter)
		{
			CharacterStartMove(swapCharacter, startPosition, startSpot);
		}

		yield return new WaitForSeconds(.5f);

		CharacterEndMove(characte, spot);
		if (swapCharacter)
			CharacterEndMove(swapCharacter, startSpot);

		end();
	}

	private void CharacterStartMove(CharacterView characte, int position, Spot spot)
	{
		characte.AnimateCharacter(SkillId.Dodge, () => { });
		characte.transform.SetParent(spot.transform);

		CharacterRotation rotation = (characte.transform.position.z - spot.transform.position.z) > 0 ? CharacterRotation.Reverse : CharacterRotation.Strict;
		characte.SetRotation(rotation);

		characte.transform.DOLocalMove(characte.Get_PositionCorrection(position), .5f);
	}

	private void CharacterEndMove(CharacterView characte, Spot spot)
	{
		characte.SetRotation(spot.GetCharacterRotation());
		characte.AnimateCharacter(SkillId.Idle, () => { });
	}

	private LockStateData[] GetLockStateData(FieldSide side, int position) => new LockStateData[1] { new LockStateData { Side = side, Index = position, State = ViewStateEnum.Hover } };
}


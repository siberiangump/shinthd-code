using System;
using System.Linq;
using System.Xml.Schema;
using UnityEngine;
using Selection = FieldSelectionType;
using State = ViewStateEnum;

public class FieldSelectIntent : MonoBehaviour
{
	[SerializeField] SelectIntent SelectIntent;
	[SerializeField] Field HeroField;
	[SerializeField] Field EnemyField;

	public void SelectOne(Field field, int fromIndex, PatternType pattern, Selection selectionType, Action<int> select, LockStateData[] lockState = null)
	{
		int[] targetIndexes = pattern.GetAsIndexes(fromIndex);
		EnableSelection(field, selectionType, targetIndexes);

		SetLockState(lockState);

		SelectIntent.SelectOne(
			field.GetSelectTargets(targetIndexes),
			(i) => Hover(field, i, targetIndexes),
			(i) => StopHover(field, i, targetIndexes),
			(i) => { DisableSelection(field); select(targetIndexes[i]); });
	}

	public void SelectAnyFrom(Field field, int fromIndex, PatternType pattern, Selection selectionType, Action select, LockStateData[] lockState = null)
	{
		int[] targetIndexes = pattern.GetAsIndexes(fromIndex);

		EnableSelection(field, selectionType, targetIndexes);

		SelectIntent.SelectAnyFrom(
			field.GetSelectTargets(),
			() => Hover(field, targetIndexes),
			() => StopHover(field, targetIndexes),
			(x) => { DisableSelection(field); select(); });
	}

	public void EnableSelection(Field field, Selection selectionType, int[] targetIndexes)
	{
		Spot[] spots = field.GetSpots();
		for (int i = 0; i < spots.Length; i++)
		{
			bool inPattern = targetIndexes.Contains(i);
			if (inPattern)
				spots[i].SetBaseState(GetSpotBaseState(spots[i], selectionType));
			else
				spots[i].SetBaseState(State.NotSelectable);
		}
	}
	public void DisableSelection(Field field)
	{
		foreach (Spot item in field.GetSpots())
			item.SetBaseState(State.Default, true);

		if (field.GetSide() == FieldSide.Enemy)
		{
			Spot[] spots = HeroField.GetSpots();
			for (int i = 0; i < spots.Length; i++)
			{
				spots[i].DisableLock();
				spots[i].SetBaseState(State.Default);
			}
		}
	}

	public void EnableHightLight(LockStateData[] lockStateData)
	{
		foreach (var item in HeroField.GetSpots())
			item.DisableLock();
		foreach (var item in EnemyField.GetSpots())
			item.DisableLock();

		foreach (var item in lockStateData)
		{
			Hover(
				field: item.Side == FieldSide.Hero ? HeroField : EnemyField,
				relativeIndex: item.Index,
				indexMap: new int[] { 0, 1, 2, 3, 4, 5 }
			);
		}
	}

	public void DisableSelection()
	{
		foreach (Spot item in HeroField.GetSpots())
			item.SetBaseState(State.Default, true);

		foreach (Spot item in EnemyField.GetSpots())
			item.SetBaseState(State.Default, true);
	}

	private void Hover(Field field, int relativeIndex, int[] indexMap)
	{
		int index = indexMap[relativeIndex];
		int overlap = field.GetSide() == FieldSide.Hero ? OverlapIndex(index) : OverlapIndexEnemy(index);

		Spot[] spots = field.GetSpots();
		for (int i = 0; i < spots.Length; i++)
		{
			State state = State.NotHover;
			if (i == overlap)
				state = State.NotHoverOverlap;
			if (i == index)
				state = State.Hover;
			spots[i].SetState(state);
		}

		if (field.GetSide() == FieldSide.Enemy)
		{
			overlap = OverlapIndexEnemyOnHeroField(index);
			if (overlap != -1)
				HeroField.GetSpot(overlap).SetState(State.NotHoverOverlap, true);
		}
	}

	private void Hover(Field field, int[] index)
	{
		int[] overlap = new int[index.Length];
		for (int i = 0; i < index.Length; i++)
			overlap[i] = index[i];

		Spot[] spots = field.GetSpots();
		for (int i = 0; i < spots.Length; i++)
		{
			State state = State.NotHover;
			if (overlap.Contains(i))
				state = State.NotHoverOverlap;
			if (index.Contains(i))
				state = State.Hover;
			spots[i].SetState(state);
		}
	}

	private void StopHover(Field field, int relativeIndex, int[] indexMap)
	{
		Spot[] spots = field.GetSpots();
		for (int i = 0; i < spots.Length; i++)
			spots[i].ResetState();

		if (field.GetSide() == FieldSide.Enemy)
		{
			spots = HeroField.GetSpots();
			for (int i = 0; i < spots.Length; i++)
				spots[i].ResetState(true);
		}
	}

	private void StopHover(Field field, int[] index)
	{
		Spot[] spots = field.GetSpots();
		for (int i = 0; i < spots.Length; i++)
			spots[i].ResetState();
	}

	private void SetLockState(LockStateData[] lockState)
	{
		if (lockState == null)
			return;
		foreach (var item in lockState)
		{
			if (item.Side == FieldSide.Hero) { }
			HeroField.GetSpot(item.Index).SetBaseState(item.State, true);
			if (item.Side == FieldSide.Enemy)
				EnemyField.GetSpot(item.Index).SetBaseState(item.State, true);
		}
	}

	State GetSpotBaseState(Spot s, Selection selectionType) => selectionType switch
	{
		Selection.Any => State.Selectable,
		Selection.Characters => s.HaveCharacter() ? State.Selectable : State.NotSelectable,
		Selection.NoCharacters => !s.HaveCharacter() ? State.Selectable : State.NotSelectable,
		_ => State.NotSelectable
	};

	int OverlapIndex(int index) => index switch
	{
		0 => 3,
		1 => 4,
		2 => 5,
		_ => -1
	};

	int OverlapIndexEnemy(int index) => index switch
	{
		3 => 0,
		4 => 1,
		5 => 2,
		_ => -1
	};

	int OverlapIndexEnemyOnHeroField(int index) => index switch
	{
		0 => 0,
		1 => 1,
		2 => 3,
		_ => -1
	};
}

public enum FieldSelectionType { Characters, NoCharacters, Any }

public enum FieldSide { Hero, Enemy }

public struct LockStateData
{
	public FieldSide Side;
	public int Index;
	public State State;
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShootIntent : MonoBehaviour
{
	public SkillId Name = SkillId.PistolShoot;

	[SerializeField] FieldSelectIntent FieldSelectIntent;
	[SerializeField] DiceThrowIntent DiceThrowIntent;

	[SerializeField] Field EnemyField;


	ProcessChainPlayer ChainPlayer = new ProcessChainPlayer();

	public void ChooseTarget(CharacterView hero, Action onEnd)
	{
		int position = hero.GetSpotIndex();
		FieldSelectIntent.SelectOne(
			EnemyField,
			position,
			PatternType.AllOponentsField,
			FieldSelectionType.Characters,
			(i) => Do(hero, EnemyField.GetSpot(i).GetCheracter(), onEnd),
			GetLockStateDatas(FieldSide.Hero, position)
		);
	}

	public void Do(CharacterView hero, CharacterView target, Action onEnd)
	{
		FieldSelectIntent.EnableHightLight(GetHightlight(hero.GetSpotIndex(), target.GetSpotIndex()));

		List<Action<Action>> chain = new List<Action<Action>>();
		int diceResul = 0;
		chain.Add(x => DiceThrowIntent.ShowDicePanel(hero, x));
		chain.Add(x => ThrowDice((r) => { diceResul = r; x(); }));
		chain.Add(x => ThrowDice((r) => { diceResul = r; x(); }));
		chain.Add(x => DoShootAnimation(hero, x));
		chain.Add(x => DoDamageOrDodgeAnimation(target, diceResul, x));
		chain.Add(x => ReturnToIdle(hero, target, x));
		chain.Add(x => { FieldSelectIntent.DisableSelection(); onEnd(); x(); });
		ChainPlayer.ExecuteChain(chain);
	}

	void ThrowDice(Action<int> result)
	{
		result?.Invoke(UnityEngine.Random.Range(1, 7));
	}

	void DoShootAnimation(CharacterView rogue, Action onEnd)
	{
		rogue.AnimateCharacter(SkillId.PistolShoot, onEnd);
	}

	void DoDamageOrDodgeAnimation(CharacterView target, int diceResult, Action onEnd)
	{
		//target.AnimateCharacter(SkillId.Dead, onEnd);
		if (diceResult > 1)
			target.AnimateCharacter(SkillId.GetDamage, onEnd);
		else
			target.AnimateCharacter(SkillId.Dodge, onEnd);
	}

	void ReturnToIdle(CharacterView rogue, CharacterView target, Action onEnd)
	{
		rogue.AnimateCharacter(SkillId.Idle, () => { });
		target.AnimateCharacter(SkillId.Idle, () => { });
		onEnd();
	}

	private LockStateData[] GetLockStateDatas(FieldSide side, int position)
	{
		LockStateData[] lockStateData = new LockStateData[6];
		for (int i = 0; i < lockStateData.Length; i++)
		{
			lockStateData[i] = new LockStateData
			{
				Index = i,
				Side = side,
				State = i == position ? ViewStateEnum.Hover : ViewStateEnum.NotSelectable
			};
		}
		return lockStateData;
	}

	private LockStateData[] GetHightlight(int indexShoot, int indexTakeDamage)
	{
		LockStateData[] lockStateData = new LockStateData[2];

		lockStateData[0] = new LockStateData
		{
			Index = indexShoot,
			Side = FieldSide.Hero,
			State = ViewStateEnum.Hover
		};
		lockStateData[1] = new LockStateData
		{
			Index = indexTakeDamage,
			Side = FieldSide.Enemy,
			State = ViewStateEnum.Hover
		};

		return lockStateData;
	}
}

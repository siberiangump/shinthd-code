using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerStrikeIntent : MonoBehaviour
{
	public SkillId Name = SkillId.HammerStrike;
	public PatternType Pattern = PatternType.LineInForward;

	[SerializeField] FieldSelectIntent FieldSelectIntent;
	[SerializeField] Field EnemyField;
	[SerializeField] DicePanel DicePanel;

	ProcessChainPlayer ChainPlayer = new ProcessChainPlayer();

	public void SelectAndDo(CharacterView hero, Action onEnd)
	{
		FieldSelectIntent.SelectOne(
			EnemyField,
			hero.GetSpotIndex(),
			Pattern,
			FieldSelectionType.Characters,
			(i) => Do(hero, EnemyField.GetSpot(i).GetCheracter(), onEnd)
		);
	}

	private void Do(CharacterView rogue, CharacterView target, Action onEnd)
	{
		List<Action<Action>> chain = new List<Action<Action>>();
		int diceResul = 0;
		chain.Add(x => ThrowDice((r) => { diceResul = r; x(); }));
		chain.Add(x => DoStrikeAnimation(rogue, x));
		chain.Add(x => DoDamageOrDodgeAnimation(target, diceResul, x));
		chain.Add(x => ReturnToIdle(rogue, target, x));
		chain.Add(x => { onEnd(); x(); });
		ChainPlayer.ExecuteChain(chain);
	}

	void ThrowDice(Action<int> result)
	{
		result?.Invoke(UnityEngine.Random.Range(1, 7));
	}

	void DoStrikeAnimation(CharacterView rogue, Action onEnd)
	{
		rogue.AnimateCharacter(SkillId.HammerStrike, onEnd);
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
	}

}

using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FlameBreathIntent : MonoBehaviour
{
	[Header("Skill Settings")]
	public SkillId Name = SkillId.FlameBreath;
	public PatternType Pattern = PatternType.LineInForward; 

	[SerializeField] Field EnemyField;
	[SerializeField] Animator[] EffectAnimation;
	[SerializeField] FieldSelectIntent SelectIntent;

	ProcessChainPlayer ChainPlayer = new ProcessChainPlayer();

	public void ChooseTarget(CharacterView hero, Action onEnd)
	{
		int[] targetIndexes = Pattern.GetAsIndexes(hero.GetSpotIndex());
		SelectIntent.SelectAnyFrom(
			EnemyField,
			hero.GetSpotIndex(),
			Pattern,
			FieldSelectionType.Any, 
			() => Do(hero, EnemyField.GetCharacters(targetIndexes), onEnd));
	}


	public void Do(CharacterView mage, CharacterView[] targets, Action onEnd)
	{
		List<Action<Action>> chain = new List<Action<Action>>();
		int diceResul = 0;
		chain.Add(x => ThrowDice((r) => { diceResul = r; x(); }));
		chain.Add(x => DoCastAnimation(mage, x));
		chain.Add(x => DoEffectAnimation(0, x));
		chain.Add(x => DoDamageOrDodgeAnimation(targets[0], diceResul, x));
		chain.Add(x => HideEffectAnimation(0, x));

		chain.Add(x => DoEffectAnimation(1, x));
		chain.Add(x => DoDamageOrDodgeAnimation(targets[1], diceResul, x));
		chain.Add(x => HideEffectAnimation(1, x));

		chain.Add(x => ReturnToIdle(mage, targets, x));
		chain.Add(x => { onEnd(); x(); });
		ChainPlayer.ExecuteChain(chain);
	}

	public void ThrowDice(Action<int> result)
	{
		result?.Invoke(UnityEngine.Random.Range(1, 7));
	}

	public void DoEffectAnimation(int index, Action action) 
	{
		EffectAnimation[index].gameObject.SetActive(true);
		EffectAnimation[index].Play("Mage_FlameBreath_Effect");
		float f = 0 ;
		DOTween.To(() => f, x => f = x, 1, 1).OnComplete(()=>action());
	}

	public void HideEffectAnimation(int index, Action action)
	{
		EffectAnimation[index].gameObject.SetActive(false);
		action.Invoke();
		//DG.Tweening.DOTween.
	}

	public void DoCastAnimation(CharacterView mage, Action onEnd)
	{
		mage.AnimateCharacter(SkillId.FlameBreath, onEnd);
	}

	public void DoDamageOrDodgeAnimation(CharacterView target, int diceResult, Action onEnd)
	{
		if (target == null)
		{
			onEnd();
			return;
		}
		if (diceResult > 1)
			target.AnimateCharacter(SkillId.GetDamage, onEnd);
		else
			target.AnimateCharacter(SkillId.Dodge, onEnd);
	}

	public void ReturnToIdle(CharacterView mage, CharacterView[] targets, Action onEnd)
	{
		mage.AnimateCharacter(SkillId.Idle, () => { });
		for (int i = 0; i < targets.Length; i++)
		{
			if (targets[i] == null)
				continue;
			targets[i].AnimateCharacter(SkillId.Idle, () => { });
		}
		onEnd();
	}

}

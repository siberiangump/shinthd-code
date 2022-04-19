using UnityEngine;
using System.Linq;
using System;
using System.Collections;

public class CharacterAnimation : MonoBehaviour
{
	[SerializeField] Animator Animator;

	[Header("TEST")]
	[SerializeField] CharacterId CharacterId;
	[SerializeField] SkillId Skill;

	CallbackHolder CallbackHolder = new CallbackHolder();

	[ContextMenu("TestPlay")]
	public void TestPlay()
	{
		PlayAnimation(CharacterId, Skill, () => { });
	}

	public void PlayAnimation(CharacterId characterId, SkillId id, Action onEnd)
	{
		string animationName = $"{characterId}_{id}";
		if (string.IsNullOrEmpty(animationName)) 
		{
			Debug.LogError($"no animation for {id}");
			onEnd?.Invoke();
			return;
		}
		CallbackHolder.SetCallback(onEnd);

		Animator.Play(animationName);
		StartCoroutine(CallOnClipEnds(Animator, CallbackHolder.Invoke));
		//Animator.GetCurrentAnimatorClipInfo(0).Length;
	}

	private IEnumerator CallOnClipEnds(Animator animator,Action onEnd) 
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(1);
		onEnd();
	}
}

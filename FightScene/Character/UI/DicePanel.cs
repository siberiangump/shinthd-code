using System;
using System.Collections;
using UnityEngine;

public class DicePanel : MonoBehaviour
{
	[SerializeField] Die HitDie;
	[SerializeField] Die DamageDie;
	[SerializeField] Animator Animator;

	[ContextMenu("Throw")]
	public void Throw(Action action) 
	{
		ThrowDie(UnityEngine.Random.Range(0,6), UnityEngine.Random.Range(0, 6), action);
	}

	public void ThrowDie(int hit, int damage, Action action) 
	{
		StartCoroutine(DoThrow(hit,damage, action));
	}

	IEnumerator DoThrow(int hit, int damage, Action action) 
	{
		Animator.Play("Show");
		yield return new WaitForSeconds(0.2f);
		HitDie.Trow(hit);
		yield return new WaitForSeconds(0.2f);
		DamageDie.Trow(damage);
		yield return new WaitForSeconds(3f);
		Animator.Play("Hide");
		action.Invoke();
	}
}

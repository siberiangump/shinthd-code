using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
	[SerializeField] int RotationsAmount;
	[SerializeField] float TickTime;
	[SerializeField] Vector3[] Sides;

	[ContextMenu("Throw")]
	public void TestThrow() 
	{
		Trow(Random.Range(0, 6));
	}

	public void Trow(int number) 
	{
		Debug.Log($"throw number {number+1}");
		StartCoroutine(Throw(number));
	}

	private IEnumerator Throw(int result) 
	{
		int times = RotationsAmount;
		while (times > 0) 
		{
			yield return new WaitForSeconds(TickTime);
			times--;
			this.transform.localRotation = Random.rotation;
		}
		this.transform.localRotation = Quaternion.Euler(Sides[result]);
	}
}

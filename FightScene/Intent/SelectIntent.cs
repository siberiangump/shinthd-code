using System;
using System.Collections;
using UnityEngine;

public class SelectIntent : MonoBehaviour
{
	[SerializeField] Camera Camera;

#region One
	public void SelectOne(ISelectIntentTarget[] selectIntentTargets, Action<int> onHover, Action<int> onStopHover, Action<int> onSelect, bool emptySelect = false)
	{
		SelectData[] targets = new SelectData[selectIntentTargets.Length];
		for (int i = 0; i < selectIntentTargets.Length; i++)
			targets[i] = selectIntentTargets[i].GetSelectData();
		StartCoroutine(SelectOne(targets, onHover, onStopHover, onSelect, emptySelect));
	}

	IEnumerator SelectOne(SelectData[] targets, Action<int> onHover, Action<int> onStopHover, Action<int> onSelect, bool emptySelect)
	{
		while (Input.GetMouseButton(0))
			yield return new WaitForEndOfFrame();

		bool selected = false;
		int hover = -1;
		do
		{
			yield return new WaitForEndOfFrame();
			int newHover = hoverOneCheck(targets, hover);
			if (newHover != hover) 
			{
				onStopHover(hover);
				hover = newHover;
				if (hover != -1)
					onHover(hover);
			}
			selected = clickCheck(onSelect, hover);
		}
		while (selected == false);

		int hoverOneCheck(SelectData[] targets, int hover)
		{
			Ray camRay = Camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(camRay, out hit, 100) == false)
				return -1;
			for (int i = 0; i < targets.Length; i++)
			{
				if (targets[i].Collider == hit.collider)
					return i;
			}
			return -1;
		}
		bool clickCheck(Action<int> onSelect, int hoverIndex)
		{
			if (hoverIndex == -1 && !emptySelect)
				return false;
			if (Input.GetMouseButton(0))
			{
				onSelect(hoverIndex);
				return true;
			}
			return false;
		}
	}
#endregion

#region AnyFrom  
	public void SelectAnyFrom(ISelectIntentTarget[] selectIntentTargets, Action onHover, Action onSpotHover, Action<bool> onSelect, bool emptySelect = false)
	{
		SelectData[] targets = new SelectData[selectIntentTargets.Length];
		for (int i = 0; i < selectIntentTargets.Length; i++)
			targets[i] = selectIntentTargets[i].GetSelectData();
		StartCoroutine(SelectAnyFrom(targets, onHover, onSpotHover, onSelect, emptySelect));
	}

	IEnumerator SelectAnyFrom(SelectData[] targets, Action onHover, Action onSpotHover, Action<bool> onSelect, bool emptySelect)
	{
		while (Input.GetMouseButton(0))
			yield return new WaitForEndOfFrame();

		bool selected = false;
		bool hover = false;
		do
		{
			yield return new WaitForEndOfFrame();
			bool hoverTmp = hoverAnyCheck(targets);
			if (hover != hoverTmp) 
			{
				hover = hoverTmp;
				if (hover)
					onHover();
				else
					onSpotHover();
			}
			selected = clickCheck(targets, onSelect, hover);
		}
		while (selected == false);

		bool hoverAnyCheck(SelectData[] targets)
		{
			Ray camRay = Camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(camRay, out hit, 100) == false)
				return false;
			int hover = -1;
			for (int i = 0; i < targets.Length; i++)
				if (targets[i].Collider == hit.collider && hover != i)
					return true;
			return false;
		}

		bool clickCheck(SelectData[] targets, Action<bool> onSelect, bool hover) 
		{
			if (!hover && !emptySelect)
				return false;
			if (Input.GetMouseButton(0)) 
			{
				onSelect.Invoke(hover);
				return true;
			}
			return false;
		}
	}
#endregion

}

public interface ISelectIntentTarget
{
	SelectData GetSelectData();
}

public struct SelectData
{
	public Collider Collider;
	public ISelectIntentTarget Target;
}
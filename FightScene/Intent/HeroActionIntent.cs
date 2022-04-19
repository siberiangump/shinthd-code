using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActionIntent : MonoBehaviour
{
	[SerializeField] FieldSelectIntent SelectIntent;
	[SerializeField] Field HeroField;
	CallbackHolder OnEnd = new CallbackHolder();

	public void Action(Action onEnd) 
	{
		HeroField.GetSpots();
		SelectIntent.SelectOne(
			HeroField, 
			-1,
			PatternType.AllYourField,
			FieldSelectionType.Characters, 
			OnSelect);
		OnEnd.SetCallback(onEnd);
	}

	public void OnSelect(int index) 
	{
		if (index == -1)
			index = -1;
		Spot spot = HeroField.GetSpot(index);
		spot.GetComponentInChildren<CharacterView>().Action(OnActionEnd);
	}

	public void OnActionEnd() 
	{
		OnEnd.Invoke();
	}
}

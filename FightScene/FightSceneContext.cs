using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSceneContext : MonoBehaviour
{
	[SerializeField] Camera CharacterCamera;
	[SerializeField] Field PlayerField;
	[SerializeField] Field EnemyField;

	[SerializeField] UIReference UI;

	public void Init(FightAssetContext assetContext) 
	{

	}


	[System.Serializable]
	public class UIReference 
	{
		public APPanel APPanel;
		public TurnPanel TurnPanel;
	}
}

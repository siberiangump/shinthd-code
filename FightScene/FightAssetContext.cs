using UnityEngine;

public class FightAssetContext : MonoBehaviour
{
	[SerializeField] FightState FightState;

	public void Init() 
	{

	}

	public FightState GetFightState() => FightState;
}

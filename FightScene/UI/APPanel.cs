using UnityEngine;
using UnityEngine.UI;

public class APPanel : MonoBehaviour
{
	[SerializeField] Image[] AP;

	public void Init() 
	{

	}

	public void SetAP(int value) 
	{
		Draw(value);
	}

	public void Draw(int value) 
	{
		for (int i = 0; i < AP.Length; i++)
		{
			AP[i].enabled = value >= i;
		}
	}
}

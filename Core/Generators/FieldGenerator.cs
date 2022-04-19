using UnityEngine;
using UnityEditor;

public class FieldGenerator
{
	public BattleFieldModel GenerateInitialField(FightConfig generationConfig)
	{
		return new BattleFieldModel
		{
			Spot = new int[6]
			{
				ModelConstants.Empty, ModelConstants.Empty, ModelConstants.Empty,
				ModelConstants.Empty, ModelConstants.Empty, ModelConstants.Empty
			}
		};
	}
}
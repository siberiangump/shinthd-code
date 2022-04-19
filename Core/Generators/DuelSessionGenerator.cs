using UnityEngine;
using System.Collections.Generic;

public class DuelSessionGenerator
{
    public FightState GenerateInitialSessionModel()
    {
        FightState session = new FightState()
        {
            Player = null,
            Opponent = null,
            History = new List<TurnActionModel>(),
            Turn = Random.value > .5f ? TurnState.Player : TurnState.Opponent
        };
        return session;
    }
}
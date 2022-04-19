using System.Collections.Generic;
using System.Diagnostics;

[System.Serializable]
public class FightState
{
    public Party Player;
    public Party Opponent;
    public List<TurnActionModel> History;
    public TurnState Turn;
    public int AP;
}
public enum TurnState { Player, Opponent };

public static class DuelSessionModelExtention
{
    public static Party GetPlayer(this FightState fight)
    {
        return fight.Turn == TurnState.Player ? fight.Player : fight.Opponent;
    }

    public static Party GetOpponent(this FightState fight)
    {
        return fight.Turn == TurnState.Player ? fight.Opponent : fight.Player;
    }

    public static Character GetCharacterAtSpot(this FightState fight, TargetSpot targetSpot) 
    {
        switch (targetSpot.SpotType)
        {
            case SpotType.PlayerField: return fight.GetPlayer().Characters[fight.GetPlayer().Field.Spot[targetSpot.Position]];
            case SpotType.OpponentField: return fight.GetOpponent().Characters[fight.GetOpponent().Field.Spot[targetSpot.Position]];
            default: UnityEngine.Debug.LogError("you try to get hero model in creature method"); return null;
        }
    }
}
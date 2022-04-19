using System.Collections.Generic;

public class PassTurnCommand
{
    public FightState PassTurn(FightState fight)
    {
        fight.Turn = fight.Turn == TurnState.Player ? TurnState.Opponent : TurnState.Player;
        ApplyPlayerFieldCardEffects(fight);
        ApplyOpponentFieldCardEffects(fight);
        CheckDead(fight);
        return fight;
    }

    private void ApplyPlayerFieldCardEffects(FightState fight)
    {
        Party playerModel = fight.Turn == TurnState.Player ? fight.Player : fight.Opponent;
        foreach (Character card in playerModel.Characters)
        {
            if(card.Effects == null)
                continue;
            DecreaseEffects(card.Effects);
        }
    }

    private void ApplyOpponentFieldCardEffects(FightState fight)
    {
        Party opponentModel = fight.Turn == TurnState.Player ? fight.Opponent : fight.Player;
        foreach (Character card in opponentModel.Characters)
        {
            // do cho to
        }
    }

    private void CheckDead(FightState fight) 
    {
        Party opponentModel = fight.Turn == TurnState.Player ? fight.Opponent : fight.Player;
        foreach (Character card in opponentModel.Characters)
        {
            if (card.Dead) { }
        }
        Party playerModel = fight.Turn == TurnState.Player ? fight.Player : fight.Opponent;
        foreach (Character card in playerModel.Characters)
        {
            if (card.Dead) { }
        }
    }

    private void DecreaseEffects(List<EffectStatus> effectStatus)
    {
        for (int i = effectStatus.Count - 1; i >= 0; i--)
        {
            if (effectStatus[i].Ended) 
            {
                effectStatus.RemoveAt(i);
                continue;
            }
            effectStatus[i].ForTurns -= 1;
            if (effectStatus[i].ForTurns <= 0)
                effectStatus[i].Ended = true;
        }
    }
}
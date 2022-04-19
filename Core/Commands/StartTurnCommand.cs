using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class StartTurnCommand
{
    public FightState StartTurn(FightState fight)
    {
        //AddManaToPlayer(fight);
        //ApplyPlayerFieldCardEffects(fight);
        //ApplyOpponentFieldCardEffects(fight);
        return fight;
    }

    private void AddManaToPlayer(FightState fight)
    {
        //Party playerModel = fight.Turn == TurnState.PlayerOne ? fight.PlayerOne : fight.PlayerTwo;
        //foreach (CardSetModel set in playerModel.Deck.CardSets)
        //    set.Mana++;
    }

    private void ApplyPlayerFieldCardEffects(FightState fight)
    {
        //Party playerModel = fight.Turn == TurnState.PlayerOne ? fight.PlayerOne : fight.PlayerTwo;
        //foreach (Character card in playerModel.Field.Spot)
        //{
        //    if (card.Effects != null)
        //        DecreaseEffects(card.Effects);
        //}
    }

    private void ApplyOpponentFieldCardEffects(FightState fight)
    {
        //Party opponentModel = fight.Turn == TurnState.PlayerOne ? fight.PlayerTwo : fight.PlayerOne;
        //foreach (Character card in opponentModel.Field.Spot)
        //{
            // do cho to
        //}
    }

    private void DecreaseEffects(List<EffectStatus> effectStatus)
    {
        //for (int i = effectStatus.Count - 1; i >= 0; i--)
        //{
        //    if (effectStatus[i].Ended)
        //        effectStatus.RemoveAt(i);
        //}
    }
}

public class PlayableCardsCommand
{
    public PlayableCardsMask GetPlayebleCardsMask(FightState fight/*, BalanceModel cardBalance)*/)
    {
        PlayableCardsMask playebleCardsMask = new PlayableCardsMask();
        Party player = fight.GetPlayer();
        Party opponent = fight.GetOpponent();
        //playebleCardsMask.PlayerField = GetPlayerFieldInteractions(player.Characters, cardBalance);
        //playebleCardsMask.OpponentField = GetOpponentFieldInteractions(opponent.Characters, cardBalance);
        return playebleCardsMask;
    }
    /*
    private IInteractionData[] GetOpponentFieldInteractions(Character[] targets, BalanceModel cardBalance)
    {
        IInteractionData[] interactions = new IInteractionData[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].EmptySpot)
            {
                interactions[i] = new None();
                continue;
            }
            IActionModel targetCard = cardBalance.GetCreature(targets[i].CardId);
            //TODO  logic
        }
        return interactions;
    }

    private IInteractionData[] GetPlayerFieldInteractions(Character[] targets, BalanceModel cardBalance)
    {
        IInteractionData[] interactions = new IInteractionData[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].EmptySpot && card is CreatureModel)
            {
                interactions[i] = new Place { CardId = card.Id() };
                continue;
            }
            interactions[i] = new None();
        }
        return interactions;
    }
    */
    private IInteractionData GetPlayerHeroInteractions()
    {
        return new None();
    }
}

public struct PlayableCardsMask
{
    public IInteractionData[] PlayerField;
    public IInteractionData Player;

    public IInteractionData[] OpponentField;
    public IInteractionData Opponent;
}

public interface IInteractionData
{
    InteractionType Type { get; }
}

public struct Place : IInteractionData
{
    InteractionType IInteractionData.Type => InteractionType.Place;
    public int CardId;
}

public struct None : IInteractionData
{
    InteractionType IInteractionData.Type => InteractionType.None;
}


public enum InteractionType { None, Place, DealDamage, Kill, Heal }
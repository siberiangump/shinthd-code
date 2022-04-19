public class StartGameCommand
{
    public FightState GenerateSessionData(DuelPlayersModel players, FightConfig configModel, BalanceModel cardBalanceModel)
    {
        DuelSessionGenerator SessionGenerator = new DuelSessionGenerator();
        FieldGenerator FieldGenerator = new FieldGenerator();
        PlayerGenerator PlayerGenerator = new PlayerGenerator();

        FightState sessionModel = SessionGenerator.GenerateInitialSessionModel();
        FillPlayerModel(ref sessionModel.Player, players.One.Name, players.One.Icon);
        FillPlayerModel(ref sessionModel.Opponent, players.Two.Name, players.Two.Icon);

        return sessionModel;

        void FillPlayerModel(ref Party playerModel, string name, string icon)
        {
            playerModel = PlayerGenerator.GenerateInitialPlayer(configModel, name, icon);
            playerModel.Field = FieldGenerator.GenerateInitialField(configModel);
        }
    }
}

[System.Serializable]
public class DuelPlayersModel
{
    public DuelPlayerModel One;
    public DuelPlayerModel Two;
}

[System.Serializable]
public class DuelPlayerModel
{
    public string Name;
    public string Icon;
}

public class PlayersStruct
{
    public Party Player;
    public Party Opponent;
}

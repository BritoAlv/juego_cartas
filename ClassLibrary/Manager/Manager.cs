namespace Poker;
public class Manager
{
    /// <summary>
    /// A manager is responsible for Simulate the game, 
    /// </summary>
    /// <param name="scorer"> An object of type Scorer has the ranks for playing Poker </param>
    /// <param name="bets"> In each mini_round each player receives a specified number of cards,
    /// so passing [5,1,1] will make a round composed by three mini_round of bets, receiving
    /// 5,1,1 respectively cards in each round</param>
    /// <param name="players"> Well literally the players of the game</param>
    public Manager(Scorer scorer, IGlobal_Contexto global_Contexto)
    {
        Scorer = scorer;
        Global_Contexto = global_Contexto;
    }
    private Scorer Scorer { get; }
    public IGlobal_Contexto Global_Contexto { get; }
    internal IEnumerable<Player> Players
    {
        get
        {
            return Global_Contexto.Players;
        }
    }
    public void SimulateGame()
    {
        Tools.ShowColoredMessage("Comienza la partida: \n", ConsoleColor.DarkGray);
        while (Global_Contexto.Active_Players.Count > 1)
        {
            Global_Contexto.Config();
            Ronda ronda = new Ronda(Scorer, Global_Contexto);
            Global_Contexto.Active_Players = ronda.Simulate();
        }
        Tools.ShowColoredMessage($"Winner is: {Global_Contexto.Active_Players.First().Id} \n", ConsoleColor.DarkGray);
    }
}
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
    public Manager(Scorer scorer, int[] bets , params Player[] players)
    {
        Scorer = scorer;
        Bets = bets;
        Players = players;
    }
    private Scorer Scorer { get; }
    public int[] Bets { get; }
    internal IEnumerable<Player> Players { get; private set; }
    public void SimulateGame()
    {
        Tools.ShowColoredMessage("Comienza la partida: \n", ConsoleColor.DarkGray);
        var active_players = Players.Where(x => x.Dinero > 0);
        Contexto contexto = new Contexto(Players, Bets);
        while (active_players.Count() > 1)
        {
            Ronda ronda = new Ronda(Scorer, contexto);
            contexto.Active_Players = ronda.Simulate();
        }
        Tools.ShowColoredMessage($"la partida la ganó: {active_players.First().Id} \n", ConsoleColor.DarkGray);
    }
}
using Poker;
namespace Game;
public class Manager
{
    public Manager(params Player[] players)
    {
        Players = players;
    }
    public IEnumerable<Player> Players { get; set; }
    public void SimulateGame()
    {
        Tools.ShowColoredMessage("Comienza la partida \n", ConsoleColor.DarkGray);
        var active_players = Players.Where(x => x.Dinero > 0);
        while (active_players.Count() > 1)
        {
            Ronda ronda = new Ronda(Players);
            Players = ronda.Simulate();
        }
        Tools.ShowColoredMessage($"la partida la ganó {active_players.First().Id} \n", ConsoleColor.DarkGray);
    }
}

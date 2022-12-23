namespace Poker;
public class PlayerManager
{
    internal PlayerManager(Player[] players)
    {
        Players = players;
        Active_Players = Players.ToList();
    }
    public IEnumerable<Player> Players { get; }
    public List<Player> Active_Players { get; internal set; }
}
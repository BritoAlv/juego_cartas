namespace Poker;
/// <summary>
/// This class represent the Context that a player may use to determine its decision. From the Player point the Context
/// is an immutable object.
/// </summary>
public class Global_Contexto
{
    public Global_Contexto(Ronda_Context ronda_Context, params Player[] players)
    {
        Players = players;
        Ronda_Context = ronda_Context;
        Active_Players = Players.ToList();
    }
    public IEnumerable<Player> Players { get;}
    public Ronda_Context Ronda_Context { get; }
    public List<Player> Active_Players{ get; internal set;}
}
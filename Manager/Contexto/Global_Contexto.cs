namespace Poker;
/// <summary>
/// This class represent the Context that a player may use to determine its decision. From the Player point the Context
/// is an immutable object.
/// </summary>
public class Global_Contexto
{
    internal Global_Contexto(IEnumerable<Player> players, int[] Bets_Rounds)
    {
        Players = players;
        Active_Players = Players.ToList();
        Ronda_Context = new Ronda_Context(players, Bets_Rounds);
    }

    public Bet Apuestas
    {
        get
        {
            return Ronda_Context.Apuestas;
        }
        internal set
        {
            Ronda_Context.Apuestas = value;
        }
    } 
    public IEnumerable<Player> Players { get;}
    public Ronda_Context Ronda_Context { get; }
    public List<Player> Active_Players{ get; internal set;}

}
namespace Poker;
/// <summary>
/// This class represent the Context that a player may use to determine its decision. From the Player point the Context
/// is an immutable object.
/// </summary>
public class Contexto
{
    internal Contexto(IEnumerable<Player> players, int[] Bets_Rounds)
    {
        Players = players;
        this.Bets_Rounds = Bets_Rounds;
        _apuestas = new Bet(players);
        Active_Players = Players.ToList();
    }
    private Bet? _apuestas;
    public Bet Apuestas
    { 
        get
        {
            if (_apuestas is null)
            {
                throw new Exception("IDK");
            }
            return _apuestas;
        }
        internal set
        {
            _apuestas = value;
        } 
    }
    public IEnumerable<Player> Players { get;}
    public List<Player> Active_Players{ get; internal set;}
    public int[] Bets_Rounds { get; }
}
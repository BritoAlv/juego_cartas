namespace Poker;
public class Contexto
{
    internal Contexto(IEnumerable<Player> players, int[] Bets_Rounds)
    {
        Players = players;
        this.Bets_Rounds = Bets_Rounds;
        _apuestas = new Bet(players);
        Active_Players = Players;
    }

    private Bet? _apuestas;
    public Bet Apuestas{ 
        get
        {
            if (_apuestas is null)
            {
                throw new Exception("IDK");
            }
            return _apuestas;
        }
        set
        {
            _apuestas = Apuestas;
        } 
        }
    public IEnumerable<Player> Players { get;}
    public IEnumerable<Player> Active_Players{ get; internal set;}
    public int[] Bets_Rounds { get; }
}
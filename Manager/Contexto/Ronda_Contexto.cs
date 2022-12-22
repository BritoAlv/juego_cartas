namespace Poker;
public class Ronda_Context
{
    private Bet? _apuestas;

    internal Ronda_Context(IEnumerable<Player> players, int[] bets_Rounds)
    {
        _apuestas = new Bet(players);
        Bets_Rounds = bets_Rounds;
    }

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
    public int[] Bets_Rounds { get; }
}
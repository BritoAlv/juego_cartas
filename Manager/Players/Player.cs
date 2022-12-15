using Poker;
namespace Game;
public abstract class Player : Ideable
{
    public string Id { get; }
    internal int Dinero { get; set; }
    internal Hand Hand = new Hand();
    internal abstract int realizar_apuesta(Bet apuestas, IEnumerable<Player> Players, string info_apuesta);
    protected Player(string id, int dinero)
    {
        Id = id;
        Dinero = dinero;
    }
}
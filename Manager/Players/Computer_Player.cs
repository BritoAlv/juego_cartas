using Poker;
namespace Game;

public class Computer_Player : Player
{
    public Computer_Player(string id, int dinero) : base(id, dinero)
    {
    }
    internal override int realizar_apuesta(Bet apuestas, IEnumerable<Player> Players, string info_apuesta)
    {
        var mayor_dinero = Players.Select(x => apuestas.Get_Dinero_Apostado(x)).Max();
        int apuesta = this.Dinero / 2;
        if((int)Hand.rank == 1) // has pair.
        {
            apuesta =  this.Dinero;
        }

        if ( (int)Hand.rank >= 3)
        {
            apuesta =  Math.Min(mayor_dinero, this.Dinero);
        }
        return apuesta;
    }
}

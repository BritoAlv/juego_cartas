namespace Poker;
public sealed class Computer_Player : Player
{
    public Computer_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public override int realizar_apuesta(Bet apuestas, IEnumerable<Player> Players, string info_apuesta)
    {
        var mayor_dinero = Players.Select(x => apuestas.Get_Dinero_Apostado(x)).Max();
        int apuesta = this.Dinero / 2;
        if(Hand.rank.Id == "Una Pareja") // has pair.
        {
            apuesta =  this.Dinero;
        }

        if (Hand.rank.Priority >= 3)
        {
            apuesta =  Math.Min(mayor_dinero, this.Dinero);
        }
        return apuesta;
    }
}

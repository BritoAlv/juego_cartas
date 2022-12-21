namespace Poker;
public class Computer_Player : Player, IApostador
{
    public Computer_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public virtual int realizar_apuesta(Contexto contexto)
    {
        var mayor_dinero = contexto.Players.Select(x => contexto.Apuestas.Get_Dinero_Apostado(x)).Max();
        int apuesta = this.Dinero / 2;
        if(Hand.rank.Id == "Una Pareja") // has pair.
        {
            apuesta =  this.Dinero;
        }

        if (Hand.rank.Priority >= 3)
        {
            apuesta = Math.Min(mayor_dinero, this.Dinero);
        }
        return apuesta;
    }

    public override IDecision parse_decision(Contexto contexto)
    {
        return new Apostar(this);
    }
}

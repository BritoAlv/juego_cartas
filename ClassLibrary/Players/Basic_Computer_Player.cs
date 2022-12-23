namespace Poker;
/// <summary>
/// This represent a basic computer player who knows by default how to Bet.
/// </summary>
public class Basic_Computer_Player : Player
{
    public Basic_Computer_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public override int realizar_apuesta(IGlobal_Contexto contexto)
    {
        var mayor_dinero = contexto.PlayerManager.Get_Active_Players(1).Select(x => contexto.Ronda_Contexto.Apuestas.Get_Dinero_Apostado(x)).Max();
        int apuesta = this.Dinero/10 + 1;
        if(Hand.rank.Id == "Una Pareja") // has pair.
        {
            apuesta =  this.Dinero;
        }

        if (Hand.rank.Priority >= 3)
        {
            if (mayor_dinero == 0)
            {
                return this.Dinero;
            }
            apuesta = Math.Min(mayor_dinero, this.Dinero);
        }

        if (apuesta == 0)
        {
            return 1;
        }
        return apuesta;
    }

    public override IDecision parse_decision(IGlobal_Contexto contexto)
    {
        return new Apostar(this);
    }
}

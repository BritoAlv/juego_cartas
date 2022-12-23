namespace Poker;
public sealed class Test_Player : Player
{
    public Test_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public override IDecision parse_decision(IGlobal_Contexto contexto)
    {
        if (Hand.rank.Priority == 0 && contexto.Ronda_Contexto.Apuestas.Get_Dinero_Apostado(this) != 0)
        {
            return new Pasar();
        }
        else
        {
            return new Apostar(this);
        }
    }
    public override int realizar_apuesta(IGlobal_Contexto contexto)
    {
        var mayor_dinero = contexto.PlayerManager.Get_Active_Players(1).Select(x => contexto.Ronda_Contexto.Apuestas.Get_Dinero_Apostado(x)).Max();
        int apuesta = 1;
        if (Hand.rank.Priority > 2) apuesta = Math.Min(mayor_dinero, this.Dinero);
        if (mayor_dinero > this.Dinero / 2)
        {
            if (Hand.rank.Priority >= 1) apuesta = this.Dinero / 3;
            else apuesta = this.Dinero / 10;
        }
        else
        {
            if (Hand.rank.Priority >= 1)
            {
                apuesta = this.Dinero / 2;
            }
            else
            {
                apuesta = this.Dinero / 10;
            }
        }
        if (apuesta == 0)
        {
            return 1;
        }
        return apuesta;
    }
}

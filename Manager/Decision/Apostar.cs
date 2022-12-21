namespace Poker;

public interface IApostador
{
    int realizar_apuesta(Contexto contexto);
}
internal class Apostar : IDecision
{
    public Apostar(IApostador apostador)
    {
        Apostador = apostador;
    }

    public string Id => "Apostar";

    public IApostador Apostador { get; }

    public bool DoDecision(Player player, Contexto contexto)
    {
        var apuesta_jugador = 0;
        apuesta_jugador = Apostador.realizar_apuesta(contexto);
        if (apuesta_jugador > player.Dinero || apuesta_jugador == 0)
        {
            return false;
        }
        contexto.Apuestas.Apostar(player, apuesta_jugador);
        Tools.ShowColoredMessage($"{player.Id} apostó {contexto.Apuestas.Get_Last_Apuesta(player)} \n", ConsoleColor.Yellow);
        return true;


    }
    public string Help => "La apuesta debe ser <= tu dinero y mayor que 0";
}

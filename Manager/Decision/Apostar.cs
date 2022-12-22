namespace Poker;

/// <summary>
/// Contract for a player to know how to bet. 
/// </summary>

/// <summary>
/// This defines what needs to have someone who wants to Bet.
/// </summary>
public interface IApostador
{
    int realizar_apuesta(Global_Contexto contexto);
}

/// <summary>
/// To define an IDecision we implement a class and a public interface that players that will implement this IDecision should
/// have.
/// </summary>
internal class Apostar : IDecision
{
    public Apostar(IApostador apostador)
    {
        Apostador = apostador;
    }
    public string Id => "Apostar";
    public IApostador Apostador { get; }
    public bool DoDecision(Player player, Global_Contexto contexto)
    {
        var apuesta_jugador = 0;
        apuesta_jugador = Apostador.realizar_apuesta(contexto);
        if (apuesta_jugador > player.Dinero || apuesta_jugador == 0)
        {
            return false;
        }
        contexto.Ronda_Context.Apuestas.Apostar(player, apuesta_jugador);
        Tools.ShowColoredMessage($"{player.Id} apostó { contexto.Ronda_Context.Apuestas.Get_Last_Apuesta(player)} \n", ConsoleColor.Yellow);
        return true;
    }
    public string Help => "La apuesta debe ser <= tu dinero y mayor que 0";
}

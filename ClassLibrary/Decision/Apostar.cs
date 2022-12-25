namespace Poker;
/// <summary>
/// Contract for a player to know how to bet. 
/// </summary>

/// <summary>
/// This defines what needs to have someone who wants to Bet.
/// </summary>
public interface IApostador
{
    int realizar_apuesta(IGlobal_Contexto contexto);
}
/// <summary>
/// To define an IDecision we implement a class and a public interface that players that will implement this IDecision should
/// have.
/// </summary>
internal sealed class Apostar : IDecision
{
    public Apostar(IApostador apostador)
    {
        Apostador = apostador;
    }
    public string Id => "Apostar";
    public IApostador Apostador { get; }
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        var apuesta_jugador = 0;
        apuesta_jugador = Apostador.realizar_apuesta(contexto);
        if (apuesta_jugador < player.Dinero && contexto.Ronda_Contexto.Apuestas.Get_Dinero_Apostado(player) + apuesta_jugador < contexto.Ronda_Contexto.Apuestas.Get_Max_Sum_Apuesta())
        {
            return false;
        }
        else if (apuesta_jugador > player.Dinero || apuesta_jugador == 0 )
        {
            return false;
        }
        contexto.Ronda_Contexto.Apuestas.Apostar(player, apuesta_jugador);
        Tools.ShowColoredMessage($"{player.Id} apostó { contexto.Ronda_Contexto.Apuestas.Get_Last_Apuesta(player)} \n", ConsoleColor.Yellow);
        return true;
    }
    public string Help => "La apuesta debe ser <= tu dinero , mayor que 0 y al menos igual a la mayor cantidad apostada por un jugador anteriormente a excepción de que quieras apostar todo tu dinero";
}
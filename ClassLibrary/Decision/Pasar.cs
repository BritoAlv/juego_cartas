namespace Poker;
/// <summary>
/// This represent the decision of not doing anything or pass. All the players pass in the same way so we don't need a custom interface.
/// </summary>
internal sealed class Pasar : IDecision
{
    public Pasar()
    {
    }
    public string Id => "Pasar";
    public string Help => "Para pasar debes debes haber apostado una cantidad >= a la de los demás jugadores";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        if (contexto.Ronda_Contexto.Apuestas.Get_Dinero_Apostado(player) < contexto.Ronda_Contexto.Apuestas.Get_Max_Sum_Apuesta())
        {
            return false;
        }
        contexto.Ronda_Contexto.Apuestas.Pasar(player);
        Tools.ShowColoredMessage($"{player.Id} pasó su turno \n", ConsoleColor.Yellow);
        return true;
    }
}
namespace Poker;
/// <summary>
/// This represent the decision of not doing anything or pass. All the players pass in the same way so we don't need a custom interface.
/// </summary>
internal class Pasar : IDecision
{
    public Pasar()
    {
    }
    public string Id => "Pasar";
    public string Help => "Debes haber apostado alguna cantidad inicial para pasar";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        if (contexto.Apuestas.Get_Dinero_Apostado(player) == 0)
        {
            return false;
        }
        contexto.Apuestas.Pasar(player);
        Tools.ShowColoredMessage($"{player.Id} pasó su turno \n", ConsoleColor.Yellow);
        return true;
    }
}
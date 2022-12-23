namespace Poker;

public interface IBaneador
{
    (int, Player) dinero_a_pagar(IGlobal_Contexto contexto);
}
internal class Banear : IDecision
{
    public Banear(IBaneador baneador)
    {
        Baneador = baneador;
    }
    public string Id => "Banear";
    public string Help => "Puedes Banear a un jugador pagando alguna cantidad de dinero, dependiendo de la cantidad de dinero que estes dispuesto a pagar";
    public IBaneador Baneador { get; }
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        (int, Player) decision = Baneador.dinero_a_pagar(contexto);
        int sacrificio = decision.Item1;
        if (sacrificio <= 0 && sacrificio > player.Dinero)
        {
            return false;
        }
        if (sacrificio >= player.Dinero / 1.5)
        {
            contexto.PlayerManager.Filtro_Ronda += (x => x.Id != decision.Item2.Id);
            Tools.ShowColoredMessage($"{player.Id} baneó a {decision.Item2.Id} durante una ronda \n", ConsoleColor.Yellow);
            player.Dinero -= sacrificio;
            return true;
        }
        else if (sacrificio >= player.Dinero / 2)
        {
            contexto.PlayerManager.Filtro_Mini_Ronda += (x => x.Id != decision.Item2.Id);
            Tools.ShowColoredMessage($"{player.Id} baneó a {decision.Item2.Id} durante una mini_ronda \n", ConsoleColor.Yellow);
            player.Dinero -= sacrificio;
            return true;
        }
        return false;
    }
}
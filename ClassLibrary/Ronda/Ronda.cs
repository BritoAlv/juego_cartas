namespace Poker;
/// <summary>
/// A match is nothing else that simulate Rounds until there is only one player with Money.
/// </summary>
internal class Ronda
{
    internal Ronda(Scorer scorer, IGlobal_Contexto contexto)
    {
        Global_Contexto = contexto;
        Scorer = scorer;
    }
    public Scorer Scorer { get; }
    public IEnumerable<Player> Participants => Global_Contexto.PlayerManager.Get_Active_Players(2);
    public IGlobal_Contexto Global_Contexto { get; }
    internal List<Player> Simulate()
    {        
        StartRonda();
        GetWinners( ExecuteMiniRondas(Global_Contexto.Ronda_Contexto.Contextos) );
        Global_Contexto.PlayerManager.Filtro_Ronda = new List<PlayerManager.Filtrar>();
        ShowRondaFinalState();        
        return Participants.Where(x => x.Dinero > 0).ToList();
    }
    void StartRonda()
    {
        Global_Contexto.PlayerManager.Filtro_Ronda = new List<PlayerManager.Filtrar>();
        Global_Contexto.PlayerManager.Shufle_Players();
        Tools.ShowColoredMessage("Comienza Una Nueva Ronda con los jugadores :", ConsoleColor.DarkRed);
        foreach (var player in Participants)
        {
            Tools.ShowColoredMessage(" " + player.Id + ", ", ConsoleColor.Blue);
            player.Hand = new Hand(this.Scorer);
        }
        Console.WriteLine();
    }
    IEnumerable<Player> ExecuteMiniRondas(List<Mini_Ronda_Contexto> contextos)
    {
        IEnumerable<Player> result = Enumerable.Empty<Player>();
        foreach (var contexto_config in contextos)
        {
            Global_Contexto.PlayerManager.Filtro_Mini_Ronda = new List<PlayerManager.Filtrar>();
            var mini_ronda = new MiniRonda(this.Global_Contexto, contexto_config);
            result = mini_ronda.Execute();
            if (result.Count() <= 1)
            {
                break;
            }
        }
        return result;
    }
    void GetWinners(IEnumerable<Player> finalist)
    {
        var best_hand = finalist.Select(x => x.Hand).OrderDescending().FirstOrDefault();
        if (best_hand is null)
        {
            Console.WriteLine("Nadie ganó la ronda");
            Console.WriteLine();
            return;
        }
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);
        var winners = finalist.Where(x => x.Hand.Igual(best_hand)).ToList();
        foreach (var winner in winners)
        {
            winner.Dinero = winner.Dinero + Global_Contexto.Ronda_Contexto.Apuestas.Get_Dinero_Total_Apostado() / winners.Count;
            Tools.ShowColoredMessage($"{winner.Id} con ${winner.Dinero}, ", ConsoleColor.DarkGray);
        }
        Console.WriteLine();
    }
    void ShowRondaFinalState()
    {
        foreach (var participant in Participants)
        {
            Console.WriteLine($"{participant.Id}".PadLeft(Participants.Select(x => x.Id.Length).Max()) + " " + participant.Hand + $" {participant.Hand.rank}");
        }
        foreach (var player in Participants)
        {
            player.Hand = new Hand(this.Scorer);
        }
    }
}

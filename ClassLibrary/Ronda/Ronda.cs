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
    public IGlobal_Contexto Global_Contexto { get; }
    public IEnumerable<Player> Participants => Global_Contexto.Ronda_Contexto.Participants;
    internal List<Player> Simulate()
    {
        StartRonda();
        ExecuteMiniRondas(Global_Contexto.Ronda_Contexto.Contextos);
        GetWinners();
        ShowRondaFinalState();
        return Global_Contexto.Active_Players.Where(x => x.Dinero > 0).ToList();
    }

    void StartRonda()
    {
        Tools.ShowColoredMessage("Comienza Una Nueva Ronda con los jugadores :", ConsoleColor.DarkRed);
        foreach (var player in Participants)
        {
            Tools.ShowColoredMessage(" " + player.Id + ", ", ConsoleColor.Blue);
            player.Hand = new Hand(this.Scorer);
        }
        Console.WriteLine();
    }
    void ExecuteMiniRondas(List<Mini_Ronda_Contexto> contextos)
    {
        foreach (var contexto_config in contextos)
        {
            var mini_ronda = new MiniRonda(this.Global_Contexto, contexto_config);
            mini_ronda.Execute();
        }
    }
    void GetWinners()
    {
        var best_hand = this.Participants.Select(x => x.Hand).OrderDescending().First();
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);
        var winners = Participants.Where(x => x.Hand.Equals(best_hand)).ToList();
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

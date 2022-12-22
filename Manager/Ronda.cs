namespace Poker;
/// <summary>
/// A match is nothing else that simulate Rounds until there is only one player with Money.
/// </summary>
internal class Ronda
{
    internal Ronda(Scorer scorer, Global_Contexto contexto)
    {
        Scorer = scorer;
        Global_Contexto = contexto;
    }
    public Scorer Scorer { get; }
    public Global_Contexto Global_Contexto { get; }
    public Ronda_Context Ronda_Contexto => Global_Contexto.Ronda_Context;
    public int[] Bets => Ronda_Contexto.Bets_Rounds;
    public IEnumerable<Player> Participants => Global_Contexto.Active_Players;
    internal List<Player> Simulate()
    {
        StartRonda();
        ExecuteMiniRondas(Bets);
        GetWinners();
        ShowRondaFinalState();
        return Participants.Where(x => x.Dinero > 0).ToList();
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
    void GetWinners()
    {
        var best_hand = this.Participants.Select(x => x.Hand).OrderDescending().First();
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);

        var winners = Participants.Where(x => x.Hand == best_hand).ToList();
        foreach (var winner in winners)
        {
            winner.Dinero = winner.Dinero + Global_Contexto.Apuestas.Get_Dinero_Total_Apostado()/winners.Count;
            Tools.ShowColoredMessage($"{winner.Id} con ${winner.Dinero}, ", ConsoleColor.DarkGray);
        }
        Console.WriteLine();
    }
    void ExecuteMiniRondas(params int[] cartas_repartir)
    {
        foreach (var cant_cartas in cartas_repartir)
        {
            var mini_ronda = new MiniRonda(this.Global_Contexto, cant_cartas);
            mini_ronda.Execute();
        }
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
}

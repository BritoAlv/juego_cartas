namespace Poker;
internal class Ronda
{
    internal Ronda(Scorer scorer, Contexto contexto)
    {
        Scorer = scorer;
        Contexto = contexto;
        contexto.Apuestas = new Bet(Participants);
    }
    public Scorer Scorer { get; }
    public Contexto Contexto { get; }
    public int[] Bets => Contexto.Bets_Rounds;
    public IEnumerable<Player> Participants => Contexto.Active_Players;
    internal IEnumerable<Player> Simulate()
    {
        StartRonda();
        ExecuteMiniRondas(Bets);
        GetWinners();
        ShowRondaFinalState();
        return Participants.Where(x => x.Dinero > 0);
    }
    private void ShowRondaFinalState()
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
    private void GetWinners()
    {
        var best_hand = Participants.Select(x => x.Hand).OrderDescending().First();
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);

        var winners = Participants.Where(x => x.Hand == best_hand);
        foreach (var winner in winners)
        {
            winner.Dinero += Contexto.Apuestas.Get_Dinero_Total_Apostado() / winners.Count();
            Tools.ShowColoredMessage($"{winner.Id} con ${winner.Dinero}, ", ConsoleColor.DarkGray);
        }
        Console.WriteLine();
    }
    private void ExecuteMiniRondas(params int[] cartas_repartir)
    {
        foreach (var cant_cartas in cartas_repartir)
        {
            var mini_ronda = new MiniRonda(this.Contexto, cant_cartas);
            mini_ronda.Execute(Contexto);
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

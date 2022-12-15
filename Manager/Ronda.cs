using Poker;
namespace Game;
internal class Ronda
{
    Bet Bet { get; }
    internal Ronda(IEnumerable<Player> participants)
    {
        Participants = participants;
        Bet = new Bet(Participants);
    }
    public IEnumerable<Player> Participants { get; }
    internal IEnumerable<Player> Simulate()
    {
        StartRonda();
        ExecuteMiniRondas(3, 1, 1);
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
        Console.WriteLine("\nLa ronda acaba aquí");
        foreach (var player in Participants)
        {
            player.Hand = new Hand();
        }
    }

    private void GetWinners()
    {
        var best_hand = Participants.Select(x => x.Hand).OrderDescending().First();
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);

        var winners = Participants.Where(x => x.Hand == best_hand);
        foreach (var winner in winners)
        {
            winner.Dinero += Bet.Get_Dinero_Total_Apostado() / winners.Count();
            Tools.ShowColoredMessage($"{winner.Id} con ${winner.Dinero}, ", ConsoleColor.DarkGray);
        }
        Console.WriteLine();
    }

    private void ExecuteMiniRondas(params int[] cartas_repartir)
    {
        foreach (var cant_cartas in cartas_repartir)
        {
            var mini_ronda = new MiniRonda(Participants, cant_cartas);
            mini_ronda.Execute(Bet);
        }
    }
    void StartRonda()
    {
        Tools.ShowColoredMessage("Comienza Una Nueva Ronda con los jugadores :", ConsoleColor.DarkRed);
        foreach (var player in Participants)
        {
            Tools.ShowColoredMessage(" " + player.Id + ", ", ConsoleColor.Blue);
        }
        Console.WriteLine();
    }
}

using Poker;
namespace Game;
internal class Ronda
{
    Bet Bet { get;}
    internal Ronda(IEnumerable<Player> participants)
    {
        Participants = participants;
        Bet = new Bet(Participants);
    }
    public IEnumerable<Player> Participants { get; }
    internal IEnumerable<Player> Simulate()
    {
        StartRonda();
        foreach (var cant_cartas in new List<int> { 3, 1, 1 })
        {
            var mini_ronda = new MiniRonda(Participants, cant_cartas);
            mini_ronda.Execute(Bet);
        }
        var best_hand = Participants.Select(x => x.Hand).OrderDescending().First();
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);
        foreach (var participant in Participants)
        {
            Console.WriteLine($"{participant.Id} " +  participant.Hand + $" {participant.Hand.rank}");
        }
        Console.WriteLine();
        var winners = Participants.Where(x => x.Hand == best_hand);
        foreach (var winner in winners)
        {
            winner.Dinero += Bet.Get_Dinero_Total_Apostado()/ winners.Count();
            Tools.ShowColoredMessage($"{winner.Id} con ${winner.Dinero}, ", ConsoleColor.DarkGray);
        }
        Console.WriteLine("\nLa ronda acaba aquí");
        foreach (var player in Participants)
        {
            player.Hand = new Hand();
        }
        return Participants.Where(x => x.Dinero > 0);
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

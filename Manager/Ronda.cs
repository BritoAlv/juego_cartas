using Poker;
namespace Game;
internal class Ronda
{
    int dinero_apostado { get; set; }
    internal Ronda(IEnumerable<Player> participants)
    {
        Participants = participants;
    }
    public IEnumerable<Player> Participants { get; }
    internal IEnumerable<Player> Simulate()
    {
        StartRonda();
        dinero_apostado = 0;
        foreach (var cant_cartas in new List<int> { 3, 1, 1 })
        {
            Repartir_Apuesta(cant_cartas);
        }
        var best_hand = Participants.Select(x => x.Hand).OrderDescending().First();
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);
        var winners = Participants.Where(x => x.Hand == best_hand);
        foreach (var winner in winners)
        {
            winner.Dinero += dinero_apostado / winners.Count();
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

    void Repartir_Apuesta(int cartas_repartir)
    {
        foreach (var player in Participants)
        {
            RepartCards(cartas_repartir, player);
            RealizarApuesta(player);
        }
    }
    void RealizarApuesta(Player player)
    {
        Tools.ShowColoredMessage($"Esta es la mano de {player.Id} ", ConsoleColor.Gray);
        Console.Write(player.Hand);
        Console.Write($"con ${player.Dinero} \n");
        if (player.Dinero > 0)
        {
            Console.Write("Apuesta > ");
            var apuesta = player.realizar_apuesta();
            while (apuesta > player.Dinero || apuesta == 0)
            {
                Console.WriteLine("Apuesta bien");
                Console.Write("Apuesta > ");
                apuesta = player.realizar_apuesta();
            }
            // at this point the player bets a reasonable number.
            player.Dinero -= apuesta;
            dinero_apostado += apuesta;
            Tools.ShowColoredMessage($"{player.Id} apostó {apuesta} \n", ConsoleColor.Yellow);
        }
    }
    void RepartCards(int v, Player player)
    {
        for (int i = 0; i < v; i++)
        {
            player.Hand.Draw(random.generate_random_card());
        }
    }
}

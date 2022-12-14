using Poker;
namespace Game;

public class Manager
{
    public Manager(params Player[] players)
    {
        Players = players;
    }
    public IEnumerable<Player> Players { get; set; }

    public void SimulateGame()
    {
        Tools.ShowColoredMessage("Comienza la partida \n", ConsoleColor.DarkGray);
        while(Players.Where(x => x.Dinero > 0).Count() > 1)
        {
            Players = Simulate_Ronda(Players);
        }
        Tools.ShowColoredMessage($"la partida la ganó {Players.Where(x => x.Dinero > 0).First().Id} \n", ConsoleColor.DarkGray);   
    }

    internal IEnumerable<Player> Simulate_Ronda(IEnumerable<Player> participants)
    {
        StartRonda();
        int dinero_apostado = 0;
        Repartir_Apuesta(3);
        Repartir_Apuesta(1);
        Repartir_Apuesta(1);
        var best_hand = participants.Select(x => x.Hand).OrderDescending().First();
        Tools.ShowColoredMessage("La ronda fue ganada por: ", ConsoleColor.DarkGray);
        foreach (var winner in participants.Where(x => x.Hand == best_hand))
        {
            winner.Dinero += dinero_apostado / participants.Where(x => x.Hand == best_hand).Count();
            Tools.ShowColoredMessage($"{winner.Id} con ${winner.Dinero}, ", ConsoleColor.DarkGray);
        }
        Console.WriteLine("\nLa ronda acaba aquí");
        foreach (var player in participants)
        {
            player.Hand = new Hand();
        }
        return participants.Where(x => x.Dinero > 0);

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
                // at this point the player bet a reasonable number.
                player.Dinero -= apuesta;
                dinero_apostado += apuesta;
                Tools.ShowColoredMessage($"{player.Id} apostó {apuesta} \n", ConsoleColor.Yellow);
            }
        }

        void StartRonda()
        {
            Tools.ShowColoredMessage("Comienza Una Nueva Ronda con los jugadores :", ConsoleColor.DarkRed);
            foreach (var player in participants)
            {
                Tools.ShowColoredMessage(" " + player.Id + ", ", ConsoleColor.Blue);
            }
            Console.WriteLine();
        }

        void Repartir_Apuesta(int cartas_repartir)
        {
            foreach (var player in participants)
            {
                RepartCards(cartas_repartir, player);
                RealizarApuesta(player);
            }
        }
    }

    private void RepartCards(int v, Player player)
    {
        for (int i = 0; i < v; i++)
        {
            player.Hand.Draw(random.generate_random_card());
        }
    }
}

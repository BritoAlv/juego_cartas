namespace Game;
using Poker;
internal class MiniRonda
{
    private readonly IEnumerable<Player> Participants;
    private readonly int cant_Cartas;
    public MiniRonda(IEnumerable<Player> participants, int cant_cartas)
    {
        this.Participants = participants;
        cant_Cartas = cant_cartas;
    }
    internal void Execute(Bet apuesta)
    {
        foreach (var player in Participants)
        {
            RepartCards(cant_Cartas, player);
            RealizarApuesta(player, apuesta);
        }
    }
    void RealizarApuesta(Player player, Bet apuesta)
    {
        Tools.ShowColoredMessage($"Esta es la mano de {player.Id} ", ConsoleColor.Gray);
        Console.Write(player.Hand);
        Console.Write($"con ${player.Dinero} \n");
        if (player.Dinero > 0)
        {
            var apuesta_jugador = DoBet();
            // at this point the player bets a reasonable number.
            player.Dinero -= apuesta_jugador;
            apuesta.Apostar(player, apuesta_jugador);
            Tools.ShowColoredMessage($"{player.Id} apostó {apuesta.Get_Last_Apuesta(player)} \n", ConsoleColor.Yellow);
        }
        int DoBet()
        {
            Console.Write("Apuesta > ");
            var apuesta_jugador = player.realizar_apuesta(apuesta, Participants);
            while (apuesta_jugador > player.Dinero || apuesta_jugador == 0)
            {
                Console.WriteLine("Apuesta bien");
                Console.Write("Apuesta > ");
                apuesta_jugador = player.realizar_apuesta(apuesta, Participants);
            }
            return apuesta_jugador;
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
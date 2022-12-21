namespace Poker;
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
        Console.Write("Esta es la mano de ");
        Tools.ShowColoredMessage($"{player.Id}".PadLeft(6), ConsoleColor.DarkMagenta);
        Console.Write("  " + player.Hand);
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
            var apuesta_jugador = 0;
            var flag = false;
            do
            {
                Console.Write(flag ? "Apuesta Bien > " : "Apuesta > ");
                var apuesta_string = Console.ReadLine();
                if (apuesta_string == null)
                {
                    continue;
                }
                apuesta_jugador = player.realizar_apuesta(apuesta, Participants, apuesta_string);
                flag = true;
            } while (apuesta_jugador > player.Dinero || apuesta_jugador <= 0 );
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
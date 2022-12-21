namespace Poker;
/// <summary>
/// This represent what happens inside a round.
/// </summary>
internal class MiniRonda
{
    private IEnumerable<Player> Participants => Contexto.Active_Players;
    private readonly int cant_Cartas;
    public MiniRonda(Contexto contexto, int cant_cartas)
    {
        Contexto = contexto;
        cant_Cartas = cant_cartas;
    }
    public Contexto Contexto { get; }
    internal void Execute()
    {
        foreach (var player in Participants)
        {
            RepartCards(cant_Cartas, player);
            EmpezarJugada(player);
            if (player.Dinero > 0)
            {
                JugarPlayer(player);
            }
            Console.WriteLine("-----------------------------------------------------------------");
        }
        Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine();
    }
    void EmpezarJugada(Player player)
    {
        Console.Write("Esta es la mano de ");
        Tools.ShowColoredMessage($"{player.Id}".PadLeft(6), ConsoleColor.DarkMagenta);
        Console.Write("  " + player.Hand);
        Console.Write($"con ${player.Dinero} \n");
    }
    void JugarPlayer(Player player)
    {
        IDecision decision = new InvalidDecision();
        bool flag = false;
        do
        {
            Console.Write(flag ? "Decide Bien > " : "Decide > ");
            var try_decision = player.parse_decision(this.Contexto);
            if (try_decision.Id != "InvalidDecision")
            {
                Tools.ShowColoredMessage($"Tomaste la decision de {try_decision.Id}\n", ConsoleColor.Green);
                if (try_decision.DoDecision(player, this.Contexto))
                {
                    decision = try_decision;
                }
                else
                {
                    Tools.ShowColoredMessage("Ejecuta bien tu decisión! \n", ConsoleColor.DarkRed);
                    Tools.ShowColoredMessage(try_decision.Help, ConsoleColor.DarkGray);
                    Console.WriteLine();
                }
            }
            else
            {
                Tools.ShowColoredMessage("Decisión Inválida \n", ConsoleColor.DarkRed);
            }
            flag = true;
        } while (decision.Id == "InvalidDecision");
        // at this point the player bets a reasonable number.
    }
    void RepartCards(int v, Player player)
    {
        for (int i = 0; i < v; i++)
        {
            player.Hand.Draw(random.generate_random_card());
        }
    }
}
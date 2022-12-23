namespace Poker;
/// <summary>
/// This represent what happens inside a round.
/// </summary>
internal class MiniRonda
{
    private IEnumerable<Player> Participants => Global_Contexto.Active_Players;
    public MiniRonda(IGlobal_Contexto contexto, Mini_Ronda_Contexto mini_contexto)
    {
        Global_Contexto = contexto;
        Mini_Contexto = mini_contexto;
    }
    public IGlobal_Contexto Global_Contexto { get; }
    public Mini_Ronda_Contexto Mini_Contexto { get; }
    internal void Execute()
    {
        var cartas_asignadas = new Dictionary<Ideable, List<Card>>();
        foreach (var player in Participants)
        {
            cartas_asignadas[player] = new List<Card>();
        }
        foreach (var player in Participants)
        {
            var card_result = Mini_Contexto.RepartirCartas(player, in cartas_asignadas);
            foreach (var cartas_asignada in card_result)
            {
                cartas_asignadas[player].Add(cartas_asignada);
            }
            foreach (var card in card_result)
            {
                player.Hand.Draw(card);
            }
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
            var try_decision = player.parse_decision(this.Global_Contexto);
            if (try_decision.Id != "InvalidDecision")
            {
                Tools.ShowColoredMessage($"Tomaste la decision de {try_decision.Id}\n", ConsoleColor.Green);
                if (try_decision.DoDecision(player, this.Global_Contexto))
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
}
namespace Poker;
/// <summary>
/// This represent what happens inside a round.
/// </summary>
internal class MiniRonda
{
    private IEnumerable<Player> Participants => Global_Contexto.Active_Players;
    public MiniRonda(Global_Contexto contexto, Mini_Ronda_Contexto mini_contexto)
    {
        Global_Contexto = contexto;
        Mini_Contexto = mini_contexto;
    }
    public Global_Contexto Global_Contexto { get; }
    public Mini_Ronda_Contexto Mini_Contexto { get; }
    internal void Execute()
    {
        Dictionary<Ideable,List<Card>> cards_by_player = Mini_Contexto.AssignCards(Participants);
        foreach (var player in Participants)
        {
            if(cards_by_player.ContainsKey(player))
            {
                foreach (var card in cards_by_player[player])
                {
                    player.Hand.Draw(card);
                }
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
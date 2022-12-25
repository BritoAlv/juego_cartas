namespace Poker;
/// <summary>
/// This represent what happens inside a round.
/// </summary>
internal class MiniRonda
{
    public MiniRonda(IGlobal_Contexto contexto, Mini_Ronda_Contexto mini_contexto)
    {
        Global_Contexto = contexto;
        Mini_Contexto = mini_contexto;
    }
    public IGlobal_Contexto Global_Contexto { get; }
    public IEnumerable<Player> Participants => Global_Contexto.PlayerManager.Get_Active_Players(3);
    public Mini_Ronda_Contexto Mini_Contexto { get; }
    internal IEnumerable<Player> Execute()
    {
        Global_Contexto.PlayerManager.Filtro_Mini_Ronda = null;
        foreach (var player in Participants)
        {
            if (Participants.Count() <= 1)
            {
                break;
            }
            Global_Contexto.Ronda_Contexto.CardsManager.AñadirCartas(Mini_Contexto.Repartidor, player, Mini_Contexto.Cant_Cartas);
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
        return Participants;
        
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
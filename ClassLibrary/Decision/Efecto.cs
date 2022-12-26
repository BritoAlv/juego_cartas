namespace Poker;
public class Efecto : IDecision
{
    public string Id => "Efecto";
    public string Help => "Utiliza el minilenguaje para realizar algún efecto";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        Tools.ShowColoredMessage("Mini_Lenguaje v1.0\n", ConsoleColor.Blue);
        Tools.ShowColoredMessage("Type >> ", ConsoleColor.Blue );
        var line = Console.ReadLine();
        if (line == "robar carta PC")
        {
            /*
            
            */
            Console.WriteLine($"{player.Id} le ha robado una carta a PC");
            return true;
        }
        Console.WriteLine();
        return false;        
    }
}
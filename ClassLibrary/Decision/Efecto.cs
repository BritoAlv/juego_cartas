namespace Poker;
public class Efecto : IDecision
{
    public string Id => "Efecto";
    public string Help => "Utiliza el minilenguaje para realizar algún efecto";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        Mini_Lenguaje lenguaje = new Mini_Lenguaje(player, contexto);
        Tools.ShowColoredMessage("Mini_Lenguaje v1.0\n", ConsoleColor.Blue);
        Tools.ShowColoredMessage("Type >> ", ConsoleColor.Blue );
        var line = Console.ReadLine();
        lenguaje.Evaluate(line);
        Console.WriteLine();
        return true;        
    }
}
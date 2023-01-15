namespace Poker;
public class Efecto : IDecision
{
    public string Id => "Efecto";
    public string Help => "Utiliza el minilenguaje para realizar algún efecto";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        
        Mini_Lenguaje lenguaje = new Mini_Lenguaje(contexto);
        var colector = player.Colector;
        if (colector.get_efectos.Count == 0)
        {
            Console.WriteLine("No posees efectos para realizar");
            return false;
        }
        Tools.ShowColoredMessage("Estos son los efectos de los que dispones, escribe el número del efecto que deseas realizar: \n", ConsoleColor.Blue);
        for (int i = 0; i < colector.get_efectos.Count; i++)
        {
            Tools.ShowColoredMessage(i.ToString() + ": "  + colector.get_efectos[i] + "\n", ConsoleColor.DarkGreen);
        }
        int number = -1;
        while (number < 0 || number >= colector.get_efectos.Count )
        {
            Tools.ShowColoredMessage("Escribe uno de los números anteriores: ", ConsoleColor.DarkBlue);
            int.TryParse(Console.ReadLine(), out number);
        }
        lenguaje.Evaluate(colector.get_efectos[number]);
        Console.WriteLine();
        return true;        
    }
}
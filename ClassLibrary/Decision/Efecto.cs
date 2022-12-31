namespace Poker;
public class Efecto : IDecision
{
    public string Id => "Efecto";
    public string Help => "Utiliza el minilenguaje para realizar algún efecto";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        Mini_Lenguaje lenguaje = new Mini_Lenguaje(player, contexto);
        if (player.get_efectos.Count == 0)
        {
            Console.WriteLine("No posees efectos para realizar");
            return false;
        }
        Tools.ShowColoredMessage("Estos son los efectos de los que dispones, escribe el número del efecto que deseas realizar: \n", ConsoleColor.Blue);
        for (int i = 0; i < player.get_efectos.Count; i++)
        {
            Tools.ShowColoredMessage(i.ToString() + ": "  + player.get_efectos[i] + "\n", ConsoleColor.DarkGreen);
        }
        int number = -1;
        while (number < 0 || number >= player.get_efectos.Count )
        {
            Tools.ShowColoredMessage("Escribe uno de los números anteriores: ", ConsoleColor.DarkBlue);
            int.TryParse(Console.ReadLine(), out number);
        }
        lenguaje.Evaluate(player.get_efectos[number]);
        Console.WriteLine();
        return true;        
    }
}
namespace Game;

internal class Tools
{
    public static void ShowColoredMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message, color);
        Console.ResetColor();
    }
}

namespace Poker;

internal class Tools
{
    /// <summary>
    /// Console Colored Print, using Console.Write()
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void ShowColoredMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message, color);
        Console.ResetColor();
    }
}

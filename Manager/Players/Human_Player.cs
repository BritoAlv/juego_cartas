namespace Poker;
using LenguajeAPI;

/// <summary>
/// This represent a basic Human Player who knows by default how to bet.
/// </summary>
public class Human_Player : Player
{
    public Human_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public override int realizar_apuesta(Contexto contexto)
    {
        Console.Write("Apuesta > ");
        var line = Console.ReadLine();
        if (string.IsNullOrEmpty(line))
        {
            return 0;
        }
        var a = Convert.ToInt32(API.Use_Compiler(line));
        return a;
    }
    public override IDecision parse_decision(Contexto contexto)
    {
        var decision = Console.ReadLine();
        if (decision == "Apostar")
        {
            return new Apostar(this);
        }
        return new InvalidDecision();
    }
}

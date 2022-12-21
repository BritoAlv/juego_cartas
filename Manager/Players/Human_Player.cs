namespace Poker;
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
        if (!int.TryParse(line, out var value))
        {
            return 0;
        }
        return value;
    }
    public override IDecision parse_decision(Contexto contexto)
    {
        var decision = Console.ReadLine();
        if (string.IsNullOrEmpty(decision))
        {
            return new InvalidDecision();    
        }

        if (decision.TrimEnd() == "Apostar")
        {
            return new Apostar(this);
        }
        return new InvalidDecision();
    }
}

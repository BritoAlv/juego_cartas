namespace Poker;
using LenguajeAPI;
public class Human_Player : Player, IApostador
{
    public Human_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public virtual int realizar_apuesta(Contexto contexto)
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
    public sealed override IDecision parse_decision(Contexto contexto)
    {
        var decision = Console.ReadLine();
        if (decision == "Apostar")
        {
            return new Apostar(this);
        }
        return new InvalidDecision();
    }
}

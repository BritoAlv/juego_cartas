namespace Poker;
/// <summary>
/// This represent a basic Human Player who knows by default how to bet.
/// </summary>
public class Human_Player : Player, IBaneador
{
    public Human_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public override int realizar_apuesta(IGlobal_Contexto contexto)
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
    public override IDecision parse_decision(IGlobal_Contexto contexto)
    {
        var decision = Console.ReadLine();
        if (string.IsNullOrEmpty(decision))
        {
            return new InvalidDecision();
        }
        if (decision.TrimEnd() == "Apostar")
        {
            return new Apostar(this);
            // notice that we can pase an IApostador here, not necessary the player.
        }
        if (decision.TrimEnd() == "Pasar")
        {
            return new Pasar();
        }
        if (decision.TrimEnd() == "Abandonar")
        {
            return new Abandonar();
        }

        if (decision.TrimEnd() == "Banear")
        {
            return new Banear(this);
        }
        return new InvalidDecision();
    }

    public (int, Player) dinero_a_pagar(IGlobal_Contexto contexto)
    {
        Console.Write("Sacrifica > ");
        var Money = Console.ReadLine();
        if (string.IsNullOrEmpty(Money))
        {
            return (0, this);
        }
        if (!int.TryParse(Money, out var value))
        {
            return (0, this);
        }
        Console.Write("Que jugador deseas banear: ");
        var player_name = Console.ReadLine().TrimEnd().TrimStart();
        if (contexto.PlayerManager.Get_Active_Players(1).Any(x => x.Id == player_name))
        {
            return (value, contexto.PlayerManager.Get_Active_Players(1).First(x => x.Id == player_name));
        }
        return (0, this);
    }
}

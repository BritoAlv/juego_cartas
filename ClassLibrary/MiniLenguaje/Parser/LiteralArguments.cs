namespace Poker;
public abstract class LiteralArguments : Iprintable
{
    protected LiteralArguments(List<Token> tokens)
    {
        Tokens = tokens;
    }

    public abstract string valor { get; }
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        foreach (var token in Tokens)
        {
            yield return token;
        }
    }
    public List<Token> Tokens { get; }
}

public class CardArguments : LiteralArguments
{
    public CardArguments(List<Token> tokens) : base(tokens)
    {
    }

    public override string valor => "Argumentos Carta";
}

public class PlayerArguments : LiteralArguments
{
    public PlayerArguments(List<Token> tokens) : base(tokens)
    {
    }
    public override string valor => "Argumentos Player";
} 
namespace Poker;
public abstract class LiteralExpression : Iprintable
{
    public LiteralExpression(Token open, LiteralArguments arguments, Token closed)
    {
        Open = open;
        Arguments = arguments;
        Closed = closed;
    }
    public Token Open { get; }
    public LiteralArguments Arguments { get; }
    public Token Closed { get; }
    public abstract string valor { get; }
    public virtual IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open;
        yield return Arguments;
        yield return Closed;
    }
}

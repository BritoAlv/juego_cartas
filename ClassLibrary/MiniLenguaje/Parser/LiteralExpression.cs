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
public class LiteralDescribeCard : LiteralExpression, IFindCard
{
    public LiteralDescribeCard(Token open_corchete, CardArguments arguments, Token closed_corchete) : base(open_corchete, arguments, closed_corchete)
    {
    }
    public override string valor => "CartaDescritaLiteral";
}



public class LiteralDescribePlayer : LiteralExpression, IFindPlayer
{
    public LiteralDescribePlayer(Token open_corchete, PlayerArguments playerArguments, Token closed_corchete) : base(open_corchete, playerArguments , closed_corchete)
    {
    }
    public override string valor => "PlayerDescritoLiteral";
}

public interface IFindPlayer : Iprintable
{

}
public interface IFindCard : Iprintable
{

}
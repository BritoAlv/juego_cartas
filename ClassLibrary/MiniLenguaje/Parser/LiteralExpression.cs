namespace Poker;
public abstract class LiteralExpression : Iprintable
{
    public LiteralExpression(Token open, List<Token> tokens, Token closed)
    {
        Open = open;
        Tokens = tokens;
        Closed = closed;
    }
    public Token Open { get; }
    public Token Closed { get; }
    public List<Token> Tokens { get; }
    public abstract string valor { get; }    
    public virtual IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open;
        foreach (var token in Tokens)
        {
            yield return token;
        }
        yield return Closed;
    }
}
public class LiteralDescribeCard : LiteralExpression, IFindCard
{
    public LiteralDescribeCard(Token open_corchete, List<Token> tokens, Token closed_corchete) : base(open_corchete, tokens, closed_corchete)
    {
    }
    public override string valor => "CartaDescritaLiteral";
}



public class LiteralDescribePlayer : LiteralExpression, IFindPlayer
{
    public LiteralDescribePlayer(Token open_corchete, List<Token> tokens, Token closed_corchete) : base(open_corchete, tokens, closed_corchete)
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
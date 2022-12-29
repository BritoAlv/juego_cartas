namespace Poker;

public class LiteralDescribeCard : LiteralExpression, IArgument<Card>
{
    public LiteralDescribeCard(Token open_corchete, LiteralArguments arguments, Token closed_corchete) : base(open_corchete, arguments, closed_corchete)
    {
    }
    public override string valor => "CartaDescritaLiteral";
}

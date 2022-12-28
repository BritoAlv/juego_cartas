namespace Poker;

public class LiteralDescribeCard : LiteralExpression, IFindCard
{
    public LiteralDescribeCard(Token open_corchete, CardArguments arguments, Token closed_corchete) : base(open_corchete, arguments, closed_corchete)
    {
    }
    public override string valor => "CartaDescritaLiteral";
}

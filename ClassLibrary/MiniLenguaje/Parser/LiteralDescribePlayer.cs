namespace Poker;

public class LiteralDescribePlayer : LiteralExpression, IFindPlayer
{
    public LiteralDescribePlayer(Token open_corchete, PlayerArguments playerArguments, Token closed_corchete) : base(open_corchete, playerArguments, closed_corchete)
    {
    }
    public override string valor => "PlayerDescritoLiteral";
}

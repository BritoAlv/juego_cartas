namespace Poker;

public class LiteralDescribePlayer : LiteralExpression, IArgument<Player>
{
    public LiteralDescribePlayer(Token open_corchete, LiteralArguments playerArguments, Token closed_corchete) : base(open_corchete, playerArguments, closed_corchete)
    {
    }
    public override string valor => "PlayerDescritoLiteral";
}

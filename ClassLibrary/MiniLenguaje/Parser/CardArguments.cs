namespace Poker;

public class CardArguments : LiteralArguments
{
    public CardArguments(List<Token> tokens) : base(tokens)
    {
    }
    public override string valor => "Argumentos Carta";
}

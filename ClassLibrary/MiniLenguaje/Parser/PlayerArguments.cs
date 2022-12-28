namespace Poker;

public class PlayerArguments : LiteralArguments
{
    public PlayerArguments(List<Token> tokens) : base(tokens)
    {
    }
    public override string valor => "Argumentos Player";
}

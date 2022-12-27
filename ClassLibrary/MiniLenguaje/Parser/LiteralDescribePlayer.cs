namespace Poker;
public class LiteralDescribePlayer : FindObject<Player>
{
    public LiteralDescribePlayer(Token open_llave, List<Token> tokens_description, Token closed_llave)
    {
        Open_Llave = open_llave;
        Tokens_Description = tokens_description;
        Closed_Llave = closed_llave;
        find_function = generate_func(Tokens_Description);
    }
    public Func<List<Player>, Player> find_function { get; }
    private static Func<List<Player>, Player> generate_func(List<Token> tokens)
    {
        static Player example(List<Player> cards)
        {
            return new Human_Player("bla", 30);
        }
        return example;
    }
    public string valor => "PlayerDescritoLiteral";
    public Token Open_Llave { get; }
    public List<Token> Tokens_Description { get; }
    public Token Closed_Llave { get; }

    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Llave;
        foreach (var token in Tokens_Description)
        {
            yield return token;
        }
        yield return Closed_Llave;
    }
}
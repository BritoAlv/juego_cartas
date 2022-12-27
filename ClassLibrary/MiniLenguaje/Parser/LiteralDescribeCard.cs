namespace Poker;
public class LiteralDescribeCard : FindObject<Card>
{
    public LiteralDescribeCard(Token open_corchete, List<Token> tokens_description, Token closed_corchete)
    {
        Open_Corchete = open_corchete;
        Tokens_Description = tokens_description;
        Closed_Corchete = closed_corchete;
        find_function = generate_func(Tokens_Description);
    }
    public Func<List<Card>, Card> find_function{ get; }
    private static Func<List<Card>, Card> generate_func(List<Token> tokens)
    {
        static Card example(List<Card> cards)
        {
            return new Card(CardValue.Siete, CardSuit.CorazónRojo);
        }
        return example;
    }
    public string valor => "CartaDescritaLiteral";
    public Token Open_Corchete { get; }
    public List<Token> Tokens_Description { get; }
    public Token Closed_Corchete { get; }
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Corchete;
        foreach (var token in Tokens_Description)
        {
            yield return token;
        }
        yield return Closed_Corchete;
    }
}

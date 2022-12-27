namespace Poker;

public class CompoundActionCard : FindObject<Card>
{
    public CompoundActionCard(Token open_parenthesis, Token signature, FindObject<Card> find_card, FindObject<Player> find_player, Token closed_parenthesis)
    {
        Open_parenthesis = open_parenthesis;
        Signature = signature;
        Find_card = find_card;
        Find_player = find_player;
        Closed_parenthesis = closed_parenthesis;
    }

    public Token Open_parenthesis { get; }
    public Token Signature { get; }
    public FindObject<Card> Find_card { get; }
    public FindObject<Player> Find_player { get; }
    public Token Closed_parenthesis { get; }
    public Func<List<Card>, Card> find_function => Find_card.find_function;
    public string valor => $"Acción : {Signature.Text}";
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_parenthesis;
        yield return Find_card;
        yield return Find_player;
        yield return Closed_parenthesis;
    }
}
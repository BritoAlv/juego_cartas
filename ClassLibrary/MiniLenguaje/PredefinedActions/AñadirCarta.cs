namespace Poker;

public class AñadirCarta : Return<bool>
{
    public AñadirCarta(Token open_parenthesis, Token signature, IArgument<Card> card, IArgument<Player> player, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Card = card;
        Player = player;
    }

    public IArgument<Card> Card { get; }
    public IArgument<Player> Player { get; }

    public override bool Evaluate(IGlobal_Contexto contexto)
    {
        var cards = Card.Get_Object(Enumerable.Empty<Card>(), contexto);
        var player = Player.Get_Object(contexto.PlayerManager.Get_Active_Players(2), contexto);
        contexto.Ronda_Contexto.CardsManager.AñadirCarta(player, cards);
        return true ;
    }

    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        if (Card is not null)
        {
            yield return Card;
        }
        if (Player is not null)
        {
            yield return Player;
        }
        yield return Closed_Parenthesis;
    }
}

namespace Poker;
public class RobarCarta : Return<Card>
{
    public RobarCarta(Token open_parenthesis, Token signature, IArgument<Card> card, IArgument<Player> player, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Card = card;
        Player = player;
    }
    public IArgument<Card> Card { get; }
    public IArgument<Player> Player { get; }
    public override IEnumerable<Card> Evaluate(IGlobal_Contexto contexto)
    {
        var player = Player.Get_Object(contexto.PlayerManager.Get_Active_Players(2), contexto);
        var card = Card.Get_Object(player.Hand.Cards, contexto);
        contexto.Ronda_Contexto.CardsManager.RemoverCarta(player, card);
        return new List<Card> { card };
    }

    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        var Card = Evaluate(contexto).FirstOrDefault();
        if (Card is not null)
        {
            return true;
        }
        return false;
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
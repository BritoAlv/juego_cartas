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
        HashSet<Card> obtained_cards = new HashSet<Card>();
        var players = Player.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto);
        foreach (var player in players)
        {
            var cards = Card.Get_Objects(player.Hand.Cards, contexto);
            foreach (var card in cards)
            {
                if (!obtained_cards.Contains(card))
                {
                    obtained_cards.Add(card);
                }
                contexto.Ronda_Contexto.CardsManager.RemoverCarta(player, card);
            }
        }
        return obtained_cards;
    }

    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        var Cards = Evaluate(contexto).FirstOrDefault();
        if (Cards is not null)
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
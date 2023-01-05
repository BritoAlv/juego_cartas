using Poker;

public class AñadirCarta : Return<bool>
{
    public AñadirCarta(Token open_parenthesis, Token signature, IArgument<Card> card, IArgument<Player> player, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Card = card;
        Player = player;
    }

    public IArgument<Card> Card { get; }
    public IArgument<Player> Player { get; }

    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        var cards = Card.Get_Objects(contexto.Ronda_Contexto.CardsManager.All_Cards(), contexto);
        var player = Player.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto);
        foreach (var jugador in player)
        {
            foreach (var card in cards)
            {
                contexto.Ronda_Contexto.CardsManager.AñadirCarta(jugador, card);
            }    
        }
        return new List<bool>{ true };
    }

    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).First();
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

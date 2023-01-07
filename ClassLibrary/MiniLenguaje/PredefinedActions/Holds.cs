namespace Poker;
public class Hand_Hold : Return<bool>
{
    public Hand_Hold(Token open_parenthesis, Token signature, IArgument<Hand> hand, IArgument<Player> jugador, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Hand = hand;
        Jugador = jugador;
    }
    public IArgument<Hand> Hand { get; }
    public IArgument<Player> Jugador { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        var players = Jugador.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto);
        bool flag = true;
        var hands = Hand.Get_Objects(contexto.PlayerManager.Get_Active_Players(2).Select(x => x.Hand), contexto);
        if (hands is null || hands.Count() == 0)
        {
            return new List<bool> { false };
        }
        foreach (var player in players)
        {
            if (!hands.Contains(player.Hand))
            {
                flag = false;
                break;
            }
        }
        return new List<bool> { flag };
    }

    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).First();

    }

    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Hand;
        yield return Jugador;
        yield return Closed_Parenthesis;
    }
}
public class Card_Hold : Return<bool>
{
    public Card_Hold(Token open_parenthesis, Token signature, IArgument<Card> card, IArgument<Player> jugador, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Cards = card;
        Jugador = jugador;
    }

    public IArgument<Card> Cards { get; }
    public IArgument<Player> Jugador { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        var players = Jugador.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto);
        bool flag = true;
        foreach (var player in players)
        {
            var cards = Cards.Get_Objects(contexto.Ronda_Contexto.CardsManager.All_Cards(), contexto);
            if (!player.Hand.Cards.Any(x => cards.Contains(x)))
            {
                flag = false;
                break;
            }
        }
        return new List<bool> { flag };
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).First();

    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Cards;
        yield return Jugador;
        yield return Closed_Parenthesis;
    }
}
namespace Poker;
/// <summary>
/// A Compound Action is an Effect that returns Void.
/// </summary>
public class CompoundAction : Iprintable
{
    public CompoundAction(Token open_parenthesis, Token signature, IArgument<Card>? find_card, IArgument<Player>? find_player, Token closed_parenthesis)
    {
        Open_Parenthesis = open_parenthesis;
        Signature = signature;
        Find_Card = find_card;
        Find_Player = find_player;
        Closed_Parenthesis = closed_parenthesis;
    }
    public Token Open_Parenthesis { get; }
    public Token Signature { get; }
    public IArgument<Card>? Find_Card { get; }
    public IArgument<Player>? Find_Player { get; }
    public Token Closed_Parenthesis { get; }
    public virtual string valor => "Acción Void  " + Signature.Text;
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        if (Find_Card is not null)
        {
            yield return Find_Card;
        }
        if (Find_Player is not null)
        {
            yield return Find_Player;
        }
        yield return Closed_Parenthesis;
    }
}

/// <summary>
/// An Action Card is an Effect that also returns a Card? when applied.
/// </summary>
public class ActionCard : CompoundAction, IArgument<Card>
{
    public ActionCard(Token open_parenthesis, Token signature, IArgument<Card>? find_card, IArgument<Player>? find_player, Token closed_parenthesis) : base(open_parenthesis, signature, find_card, find_player, closed_parenthesis)
    {
    }
    public override string valor => "Acción Carta : " + Signature.Text;
}

/// <summary>
/// An Action Player is an Effect that also returns a Player? when applied.
/// </summary>
public class ActionPlayer : CompoundAction, IArgument<Player>
{
    public ActionPlayer(Token open_parenthesis, Token signature, IArgument<Card>? find_card, IArgument<Player>? find_player, Token closed_parenthesis) : base(open_parenthesis, signature, find_card, find_player, closed_parenthesis)
    {
    }
    public override string valor => "Acción Player : " + Signature.Text;
}
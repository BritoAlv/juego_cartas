namespace Poker;

public abstract class CompoundAction : Iprintable
{
    public CompoundAction(Token open_parenthesis, Token signature, IFindCard? find_card, IFindPlayer? find_player, Token closed_parenthesis)
    {
        Open_Parenthesis = open_parenthesis;
        Signature = signature;
        Find_Card = find_card;
        Find_Player = find_player;
        Closed_Parenthesis = closed_parenthesis;
    }

    public Token Open_Parenthesis { get; }
    public Token Signature { get; }
    public IFindCard? Find_Card { get; }
    public IFindPlayer? Find_Player { get; }
    public Token Closed_Parenthesis { get; }

    public string valor => $"Acción {Signature.Text}";

    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Signature;
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

public class ActionCard : CompoundAction, IFindCard
{
    public ActionCard(Token open_parenthesis, Token signature, IFindCard? find_card, IFindPlayer? find_player, Token closed_parenthesis) : base(open_parenthesis, signature, find_card, find_player, closed_parenthesis)
    {

    }
}

public class voidActionCard : CompoundAction
{
    public voidActionCard(Token open_parenthesis, Token signature, IFindCard? find_card, IFindPlayer? find_player, Token closed_parenthesis) : base(open_parenthesis, signature, find_card, find_player, closed_parenthesis)
    {
    }
}

public class ActionPlayer : CompoundAction, IFindPlayer
{
    public ActionPlayer(Token open_parenthesis, Token signature, IFindCard? find_card, IFindPlayer? find_player, Token closed_parenthesis) : base(open_parenthesis, signature, find_card, find_player, closed_parenthesis)
    {

    }
}

public class voidActionPlayer : CompoundAction
{
    public voidActionPlayer(Token open_parenthesis, Token signature, IFindCard? find_card, IFindPlayer? find_player, Token closed_parenthesis) : base(open_parenthesis, signature, find_card, find_player, closed_parenthesis)
    {
    }
}

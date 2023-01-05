namespace Poker;
public class IF_Expresion : Return<bool>
{
    public IF_Expresion(Token open_parenthesis, Token signature, Return<bool> condition, Token implies, IFirst action1, Token not, IFirst action2, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Condition = condition;
        Implies = implies;
        Action1 = action1;
        Not = not;
        Action2 = action2;
    }

    public Return<bool> Condition { get; }
    public Token Implies { get; }
    public IFirst Action1 { get; }
    public Token Not { get; }
    public IFirst Action2 { get; }

    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        if (Condition.Evaluate(contexto).First())
        {
            return new List<bool> { Action1.Evaluate_Top(contexto) };
        }
        return new List<bool> { Action2.Evaluate_Top(contexto) };
    }

    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).Count() > 0;
    }

    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Condition;
        yield return Implies;
        yield return (Iprintable)Action1;
        yield return Not;
        yield return (Iprintable)Action2;
    }
}
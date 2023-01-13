namespace Poker;
public class IF_Expresion : Return<bool>
{
    public IF_Expresion(Token open_parenthesis, Token signature, Return<bool> condition, Token implies, List<IFirst> action1, Token not, List<IFirst> action2, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Condition = condition;
        Implies = implies;
        Action1 = action1;
        Not = not;
        Action2 = action2;
    }
    public Return<bool> Condition { get; }
    public Token Implies { get; }
    public List<IFirst> Action1 { get; }
    public Token Not { get; }
    public List<IFirst> Action2 { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        if (Condition.Evaluate(contexto).First())
        {
            return Action1.Select(x => x.Evaluate_Top(contexto)).ToList();
        }
        if (Action2.Count == 0)
        {
            return new List<bool> { false };
        }
        return Action2.Select(x => x.Evaluate_Top(contexto)).ToList();
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
        foreach (var action in Action1)
        {
            yield return (Iprintable)action;
        }
        yield return Not;
        foreach (var action in Action2)
        {
            yield return (Iprintable)action;
        }
        yield return Closed_Parenthesis;
    }
}
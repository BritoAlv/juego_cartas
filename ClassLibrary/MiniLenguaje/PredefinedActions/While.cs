namespace Poker;
public class While_Expresion : Return<bool>
{
    public While_Expresion(Token open_parenthesis, Token signature, Return<bool> condition, Token implies, List<IFirst> action1,  Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Condition = condition;
        Implies = implies;
        Action1 = action1;
    }
    public Return<bool> Condition { get; }
    public Token Implies { get; }
    public List<IFirst> Action1 { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        while (Condition.Evaluate(contexto).First())
        {
            Action1.Select(x => x.Evaluate_Top(contexto)).ToList();
        }
        return new List<bool> { true };
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
        yield return Closed_Parenthesis;
    }
}
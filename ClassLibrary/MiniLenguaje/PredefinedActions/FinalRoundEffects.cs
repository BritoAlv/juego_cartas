namespace Poker;
public class FinalRoundEffect : Return<bool>
{
    public FinalRoundEffect(Token open_parenthesis, Token signature, Token id, IFirst action , Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Id = id;
        Action = action;
    }
    public Token Id { get; }
    public IFirst Action { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    { 
        if (!contexto.variables.ContainsKey(Id.Text))
        {
            contexto.variables[Id.Text] = true;
            contexto.FinalRoundEffects.Add(this);
        }
        if ((bool)contexto.variables[Id.Text])
        {
            Action.Evaluate_Top(contexto);
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
        yield return Id;
        yield return (Iprintable)Action;
        yield return Closed_Parenthesis;
    }
}
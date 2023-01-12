namespace Poker;
public class Exist : Return<bool>
{
    public Exist(Token open_parenthesis, Token signature, Token nombre,  Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Nombre = nombre;
    }
    public Token Nombre { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    { 
        if (contexto.variables.ContainsKey(Nombre.Text))
        {
            return new List<bool> { true };
        }
        return new List<bool> { false };
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).Count() > 0;
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Nombre;
        yield return Closed_Parenthesis;
    }
}
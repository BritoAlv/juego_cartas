namespace Poker;
public class Asignarbool : Return<bool>
{
    public Asignarbool(Token open_parenthesis, Token signature, Token nombre, Return<bool> source, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Nombre = nombre;
        Source = source;
    }
    public Token Nombre { get; }
    public Return<bool> Source { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        contexto.variables[Nombre.Text] = Source.Evaluate(contexto).First();
        return new List<bool> { true };
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).Count() > 0;
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Nombre;
        yield return Source;
        yield return Closed_Parenthesis;
    }
}
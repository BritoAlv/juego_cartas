namespace Poker;
public class OperationInt : Return<int>
{
    public OperationInt(Token open_parenthesis, Token signature, Token argumento, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Argumento = argumento;
    }
    public Token Argumento { get; }
    public override IEnumerable<int> Evaluate(IGlobal_Contexto contexto)
    {
        string operation = Operacion.Replaace(Argumento.Text, contexto);
        return new List<int>{(int)Eval.Evaluador.Evaluator(operation)};
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).Count() > 0;
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Argumento;
        yield return Closed_Parenthesis;
    }
}
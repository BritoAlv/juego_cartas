namespace Poker;
public abstract class Return<T> : IArgument<T>, Iprintable, IFirst
{
    protected Return(Token open_parenthesis, Token signature, Token closed_parenthesis)
    {
        Open_Parenthesis = open_parenthesis;
        Signature = signature;
        Closed_Parenthesis = closed_parenthesis;
    }
    public Token Open_Parenthesis { get; }
    public Token Signature { get; }
    public Token Closed_Parenthesis { get; }
    public string valor => "Acción " + Signature.Text;
    public abstract IEnumerable<Iprintable> GetChildrenIprintables();
    public abstract T Evaluate(IGlobal_Contexto contexto);
    public T Get_Object(IEnumerable<T> list, IGlobal_Contexto contexto)
    {
        return Evaluate(contexto);
    }

    public abstract bool Evaluate_Top(IGlobal_Contexto contexto);
}

public interface IFirst // needs a better name
{
    bool Evaluate_Top(IGlobal_Contexto contexto);
}
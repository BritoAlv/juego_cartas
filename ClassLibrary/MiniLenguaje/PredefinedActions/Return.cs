namespace Poker;

public abstract class Return<T> : Accion, IArgument<T>
{
    protected Return(Token open_parenthesis, Token signature, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
    }
    public abstract T Evaluate(IGlobal_Contexto contexto);
    public T Get_Object(IEnumerable<T> list, IGlobal_Contexto contexto)
    {
        return Evaluate(contexto);
    }
}

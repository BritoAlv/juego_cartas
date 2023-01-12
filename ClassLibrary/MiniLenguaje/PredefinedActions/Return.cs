namespace Poker;
/*
This is the class I will use to create new effect predefined actions, it's generic because I need to know
the return type of the object in case I will use it as argument for other action.
*/
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
    public abstract IEnumerable<T> Evaluate(IGlobal_Contexto contexto);
    public T Get_Object(IEnumerable<T> list, IGlobal_Contexto contexto)
    {
        return Get_Objects(list, contexto).First();
    }

    public abstract bool Evaluate_Top(IGlobal_Contexto contexto);

    public IEnumerable<T> Get_Objects(IEnumerable<T> list, IGlobal_Contexto contexto)
    {
        return Evaluate(contexto);
    }
}


/*
Every action that implements this interface have to define how evaluate itself, 
when it's the principal action of the effect.
*/
public interface IFirst // needs a better name
{
    bool Evaluate_Top(IGlobal_Contexto contexto);
}
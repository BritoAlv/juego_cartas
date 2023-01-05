namespace Poker;

/*
This is the class in charge of Parsing the arguments that are literal descriptions
of objects T.
*/
public class LiteralDescribe<T> : IArgument<T> where T : IDescribable<T>, IEqualityComparer<T>
{
    public LiteralDescribe(Token open, LiteralArguments literalArguments, Token closed, Token? complemento = null)
    {
        Open = open;
        LiteralArguments = literalArguments;
        Closed = closed;
        Complemento = complemento;
    }
    public string valor => "Literal Describe" + T.Valor;
    public Token Open { get; }
    public LiteralArguments LiteralArguments { get; }
    public Token Closed { get; }
    public Token? Complemento { get; }

    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open;
        yield return LiteralArguments;
        yield return Closed;
    }

    public IEnumerable<T> Get_Objects(IEnumerable<T> list, IGlobal_Contexto contexto)
    {
        foreach (var Func in GetTFunction(LiteralArguments))
        {
            IEnumerable<T> obtained_T = Func(list);
            if (obtained_T.Count() > 0)
            {
                if (Complemento is not null)
                {
                    return obtained_T.Complementt(list);
                }
                return obtained_T;
            }
        }
        return Enumerable.Empty<T>();
    }

    private List<Func<IEnumerable<T>, IEnumerable<T>>> GetTFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<T>, IEnumerable<T>>> result = new List<Func<IEnumerable<T>, IEnumerable<T>>>();
        foreach (var argument in arguments.Descriptions)
        {
            if (argument is UnaryDescriptionArgument unary)
            {
                result.Add(T.get_T_func(unary));
            }
            else if (argument is BinaryDescriptionArgument binary)
            {
                result.Add(get_T_func(binary));
            }
        }
        return result;
    }
    private Func<IEnumerable<T>, IEnumerable<T>> get_T_func(BinaryDescriptionArgument binary)
    {
        Func<IEnumerable<T>, IEnumerable<T>> T1 = T.get_T_func(binary.Izq);
        Func<IEnumerable<T>, IEnumerable<T>> T2 = T.get_T_func(binary.Der);
        if (binary.Operador.Text == "&&")
        {
            return x => T1(x).Intersectt(T2(x));
        }
        else if (binary.Operador.Text == "||")
        {
            return x => T1(x).Unionn(T2(x));
        }
        return x => Enumerable.Empty<T>();
    }
}
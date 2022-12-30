namespace Poker;
public abstract class LiteralDescribe<T> : IArgument<T> where T : IEqualityComparer<T>
{
    public LiteralDescribe(Token open, LiteralArguments literalArguments, Token closed)
    {
        Open = open;
        LiteralArguments = literalArguments;
        Closed = closed;
    }
    public abstract string valor { get; }
    public Token Open { get; }
    public LiteralArguments LiteralArguments { get; }
    public Token Closed { get; }
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open;
        yield return LiteralArguments;
        yield return Closed;
    }
    public abstract Func<IEnumerable<T>, IEnumerable<T>> get_T_func(UnaryDescriptionArgument unary);
    public T Get_Object(IEnumerable<T> list, IGlobal_Contexto contexto)
    {
        foreach (var Func in GetTFunction(LiteralArguments))
        {
            IEnumerable<T> obtained_T = Func(list);
            if (obtained_T.Count() > 0)
            {
                return obtained_T.First();
            }
        }
        throw new Exception("No se encontró: ");
    }
    private List<Func<IEnumerable<T>, IEnumerable<T>>> GetTFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<T>, IEnumerable<T>>> result = new List<Func<IEnumerable<T>, IEnumerable<T>>>();
        foreach (var argument in arguments.Descriptions)
        {
            if (argument is UnaryDescriptionArgument unary)
            {
                result.Add(get_T_func(unary));
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
        if (binary.Operador.Text == "&&")
        {
            Func<IEnumerable<T>, IEnumerable<T>> T1 = get_T_func(binary.Izq);
            Func<IEnumerable<T>, IEnumerable<T>> T2 = get_T_func(binary.Der);
            return x => T1(x).Intersectt(T2(x));
        }
        else if (binary.Operador.Text == "||")
        {
            Func<IEnumerable<T>, IEnumerable<T>> T1 = get_T_func(binary.Izq);
            Func<IEnumerable<T>, IEnumerable<T>> T2 = get_T_func(binary.Der);
            return x => T1(x).Unionn(T2(x));
        }
        return x => Enumerable.Empty<T>();
    }
}

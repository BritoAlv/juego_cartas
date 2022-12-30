namespace Poker;

public interface IDescribable<T> where T : IDescribable<T>, IEqualityComparer<T>
{
    static abstract Func<IEnumerable<T>, IEnumerable<T>> get_T_func(UnaryDescriptionArgument unary);
    static abstract string Valor{ get; }
}

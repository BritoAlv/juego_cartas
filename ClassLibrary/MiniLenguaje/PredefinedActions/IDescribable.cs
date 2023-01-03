namespace Poker;

/*
T has to be describable, this means that given an unary argument he has to return a func
to given objects of the same type, get the one is being asked.
*/
public interface IDescribable<T> where T : IDescribable<T>, IEqualityComparer<T>
{
    static abstract Func<IEnumerable<T>, IEnumerable<T>> get_T_func(UnaryDescriptionArgument unary);
    static abstract string Valor{ get; }
}

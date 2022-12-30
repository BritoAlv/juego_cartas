namespace Poker;
public interface IArgument<T> : Iprintable
{
    T Get_Object(IEnumerable<T> list, IGlobal_Contexto contexto);
}

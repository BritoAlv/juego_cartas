namespace Poker;
/*
If something implement this can be passed as an argument to a predefined action.
*/
public interface IArgument<T> : Iprintable
{
    IEnumerable<T> Get_Objects(IEnumerable<T> list, IGlobal_Contexto contexto);
}

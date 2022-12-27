namespace Poker;
/// <summary>
/// This is the contract that should hold the actions that finds objects.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface FindObject<T> : Iprintable
{
    Func<List<T>, T> find_function { get; }
}

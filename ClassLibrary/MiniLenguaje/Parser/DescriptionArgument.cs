namespace Poker;

public abstract class DescriptionArgument : Iprintable
{
    public abstract string valor { get; }
    public abstract IEnumerable<Iprintable> GetChildrenIprintables();
}
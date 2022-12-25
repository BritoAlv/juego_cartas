namespace Poker;
public class HandRank : IComparable<HandRank>
{
    public HandRank(string id, double priority)
    {
        Id = id;
        Priority = priority;
    }
    public string Id { get; }
    public double Priority { get; }
    public int CompareTo(HandRank? other)
    {
        if (other == null)
        {
            return 1;
        }
        return this.Priority.CompareTo(other.Priority);
    }
    public bool Igual(object? obj)
    {
        if (obj is HandRank other)
        {
            if (this.Id == other.Id && this.Priority == other.Priority)
            {
                return true;
            }
        }
        return false;
    }
    public override string ToString()
    {
        return this.Id;
    }
}
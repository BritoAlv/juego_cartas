namespace Poker;

public abstract class Rank: IRank
{
    /// <summary>
    /// Priority of your rank, higher means better.
    /// </summary>
    public abstract double Priority{ get; }
    /// <summary>
    /// Given some cards determine if they have the rank you are implementing
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public abstract bool HasThisRank(IEnumerable<Card> cards);
    /// <summary>
    /// Given two hands with your rank, determine which is better, you should return :
    /// 1 if first is better.
    /// 0 if are equal.
    /// -1 if second is better.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    public abstract int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B);
    public readonly string Id;
    public override string ToString()
    {
        return Id;
    }
    public Rank(string id)
    {
        Id = id;
    }
}
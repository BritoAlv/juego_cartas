namespace Poker;

internal class CartaAltaRank : Rank
{
    public CartaAltaRank(string id) : base(id)
    {
    }
    public override double Priority => 0;
    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return RankByHighCard(A, B);
    }
    public static int RankByHighCard(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        IEnumerable<Card> Transform(IEnumerable<Card> C)
        {
            return C.OrderByDescending(x => x.Value);
        }
        var zipped = Transform(A).Zip(Transform(B));
        foreach (var tuple in zipped)
        {
            if (tuple.First.Value > tuple.Second.Value)
            {
                return 1;
            }
            else if (tuple.Second.Value > tuple.First.Value)
            {
                return -1;
            }
            else
            {
                continue;
            }
        }
        return 0;
    }
    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return true;
    }
}

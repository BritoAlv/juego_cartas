namespace Poker;

internal class TrioRank : Rank
{
    public TrioRank(string id) : base(id)
    {
    }

    public override double Priority => 4;

    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return RankByTrio(A, B);
    }

    public static int RankByTrio(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        CardValue Transform(IEnumerable<Card> C)
        {
            return C.GroupBy(x => x.Value).Where(x => x.Count() == 3).First().First().Value;
        }
        var result = Transform(A).CompareTo(Transform(B));
        if (result == 0)
        {
            return PairRank.RankByPareja(A, B);
        }
        return result;
    }

    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return EvalExtensions.HasOfAKind(cards, 3);
    }
}

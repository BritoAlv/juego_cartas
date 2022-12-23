namespace Poker;
internal class ThreePairRank : Rank
{
    public ThreePairRank(string id) : base(id)
    {
    }

    public override double Priority => 3;
    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return PairRank.RankByPareja(A, B);
    }
    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return EvalExtensions.CountOfAKind(cards, 2) == 3;
    }
}

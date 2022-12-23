namespace Poker;

internal class FullRank : Rank
{
    public FullRank(string id) : base(id)
    {
    }

    public override double Priority => 7;

    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return TrioRank.RankByTrio(A, B);
    }

    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        if (EvalExtensions.HasOfAKind(cards, 3))
        {
            return cards
            .GroupBy(x => x.Value)
            .Where(x => x.Count() == 2).Count() == 1;
        }
        return false;
    }
}

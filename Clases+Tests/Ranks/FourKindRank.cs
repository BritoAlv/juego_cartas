namespace Poker;

internal class FourKindRank : Rank
{
    public FourKindRank(string id) : base(id)
    {
    }

    public override double Priority => 8;

    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        CardValue Transform(IEnumerable<Card> C)
        {
            return C.GroupBy(x => x.Value).Where(x => x.Count() >= 4).First().First().Value;
        }
        var result = Transform(A).CompareTo(Transform(B));
        if (result == 0)
        {
            return CartaAltaRank.RankByHighCard(A, B);
        }
        return result;
    }

    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return EvalExtensions.HasOfAKind(cards, 4);
    }
}

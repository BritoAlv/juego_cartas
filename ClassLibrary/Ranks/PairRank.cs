namespace Poker;

public class PairRank : Rank
{
    public PairRank(string id) : base(id)
    {
    }
    public override double Priority => 1;
    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return RankByPareja(A, B);
    }
    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return EvalExtensions.HasOfAKind(cards, 2);
    }
    public static int RankByPareja(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        IEnumerable<CardValue> Transform(IEnumerable<Card> C)
        {
            return C.GroupBy(x => x.Value).Where(x => x.Count() == 2).Select(x => x.First().Value).OrderDescending();
        }
        var zipped = Transform(A).Zip(Transform(B));
        foreach (var tuple in zipped)
        {
            if (tuple.First > tuple.Second)
            {
                return 1;
            }
            else if (tuple.Second > tuple.First)
            {
                return -1;
            }
            else
            {
                continue;
            }
        }
        return CartaAltaRank.RankByHighCard(A, B);
    }
}
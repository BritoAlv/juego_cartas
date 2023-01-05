/*
This file contains customs classes to demonstrate extensibility.
*/
using Poker;
public class TwoTwos : Rank
{
    public TwoTwos(string id) : base(id)
    {
    }

    public override double Priority => 20;
    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return 1;
    }
    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return cards.Where(x => (int)x.Value == 2).Count() == 2;
    }
}

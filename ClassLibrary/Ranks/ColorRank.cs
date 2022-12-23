namespace Poker;

internal class ColorRank : Rank
{
    public ColorRank(string id) : base(id)
    {
    }

    public override double Priority => 6;

    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return CartaAltaRank.RankByHighCard(A, B);
    }

    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return cards.generate_combinaciones(5).Any(x => HasColor_5(x));
    }

    public static bool HasColor_5(IEnumerable<Card> cards)
    {
        if (cards.Count() < 5)
        {
            return false;
        }
        return cards.All(x => cards.First().Suit == x.Suit);
    }
}

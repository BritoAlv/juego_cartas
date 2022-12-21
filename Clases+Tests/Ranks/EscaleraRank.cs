namespace Poker;
internal class EscaleraRank : Rank
{
    public EscaleraRank(string id) : base(id)
    {
    }

    public override double Priority => 5;

    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return CartaAltaRank.RankByHighCard(A, B);
    }
    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return HasEscalera(cards);
    }

    public static bool HasEscalera(IEnumerable<Card> cards)
    {
        return cards.generate_combinaciones(5).Any(x => HasEscalera_5(x));
    }
    public static bool HasEscalera_5(IEnumerable<Card> cards)
    {
        if (cards.Count() < 5)
        {
            return false;
        }
        if (cards.Any(x => x.Value == CardValue.As))
        {
            IEnumerable<int> c = cards.Select(x => (((int)x.Value <= 5) ? (int)x.Value + 13 : (int)x.Value));
            return c.Order().SelectConsecutive((n, next) => (n + 1) == next).All(boolean => boolean);
        }
        return cards.OrderBy(card => card.Value).SelectConsecutive((n, next) => (n.Value + 1) == next.Value).All(boolean => boolean);
    }
}

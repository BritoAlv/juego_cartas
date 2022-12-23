namespace Poker;

internal class EscaleraColorRank : Rank
{
    public EscaleraColorRank(string id) : base(id)
    {
    }

    public override double Priority => 9;

    public override int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return CartaAltaRank.RankByHighCard(A, B);
    }

    public override bool HasThisRank(IEnumerable<Card> cards)
    {
        return cards.generate_combinaciones(5).Any(x => EscaleraRank.HasEscalera_5(x) && ColorRank.HasColor_5(x));
    }
}
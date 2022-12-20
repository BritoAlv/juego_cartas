namespace Poker;

internal interface IRank
{
    bool HasThisRank(IEnumerable<Card> cards);
    int CommonRanker(IEnumerable<Card> A, IEnumerable<Card> B);
    double Priority { get; }
}

using NeoSmart.Unicode;
namespace Poker;

public class Hand : IComparable<Hand>
{
    public override string ToString()
    {
        var result = "";
        foreach (var card in Cards.OrderByDescending(x => ((int)x.Value == 1) ? 15 : (int)x.Value))
        {
            result = result + card.ToString();
        }
        return result;
    }
    public HandRank rank => this.Scorer.GetHandRank(Cards);
    public string ToStringWithRank() => this.ToString() + " => " + Scorer.GetHandRank(Cards);
    private List<Card> _cards;
    public Hand(Scorer scorer)
    {
        Scorer = scorer;
        _cards = new List<Card>();
    }

    public IEnumerable<Card> Cards
    {
        get
        {
            return this._cards;
        }
    }
    public Scorer Scorer { get; }
    public int CompareTo(Hand? other)
    {
        if (other is null)
        {
            return 1;
        }
        if (this.rank.CompareTo(other.rank) == 0)
        {
            var common_rank = Scorer.GetHandRank(this.Cards);
            return Scorer.Common_Ranker(common_rank, this.Cards, other.Cards);
        }
        else
        {
            return this.rank.CompareTo(other.rank);
        }
    }
    public void Draw(Card card) => _cards.Add(card);

    public override bool Equals(object? obj)
    {
        if (obj is Hand other)
        {
            if (this.Cards.Count() != other.Cards.Count())
            {
                return false;
            }
            IEnumerable<bool> comparer = this.Cards.Zip(other.Cards, (x, y) => x.Equals(y));
            return comparer.All(x => x == true);
        }
        return false;
    }
}

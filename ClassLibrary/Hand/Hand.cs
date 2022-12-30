using System.Diagnostics.CodeAnalysis;
using NeoSmart.Unicode;
namespace Poker;

public partial class Hand : IComparable<Hand>, IDescribable<Hand>, IEqualityComparer<Hand>
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

    public static string Valor => "Mano";

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
    internal void Draw(Card card) => _cards.Add(card);
    internal Card Remove(Predicate<Card> pred)
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            if (pred(_cards[i]))
            {
                var card = _cards[i];
                _cards.RemoveAt(i);
                return card;
            }
        }
        return _cards[0];
    }
    public bool Igual(object? obj)
    {
        if (obj is Hand other)
        {
            if (this.Cards.Count() != other.Cards.Count())
            {
                return false;
            }
            IEnumerable<bool> comparer = this.Cards.Zip(other.Cards, (x, y) => x.Iguales(y));
            return comparer.All(x => x == true);
        }
        return false;
    }

    public bool Equals(Hand? x, Hand? y)
    {
        if (x is null || y is null)
        {
            return false;
        }
        return x.Igual(y);
    }

    public int GetHashCode([DisallowNull] Hand obj)
    {
        return obj.Cards.GetHashCode();
    }
}

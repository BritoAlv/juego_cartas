using NeoSmart.Unicode;
namespace Poker
{
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
        public HandRank rank => Scorer.GetHandRank(Cards);
        public string ToStringWithRank() => this.ToString() + " => " + Scorer.GetHandRank(Cards);
        private List<Card> _cards = new List<Card>();
        public IEnumerable<Card> Cards
        {
            get
            {
                return this._cards;
            }
        }
        public int CompareTo(Hand? other)
        {
            if (Scorer.GetHandRank(this.Cards) > Scorer.GetHandRank(other.Cards))
            {
                return 1;
            }
            else if (Scorer.GetHandRank(this.Cards) < Scorer.GetHandRank(other.Cards))
            {
                return -1;
            }
            else
            {
                var common_rank = Scorer.GetHandRank(this.Cards);
                return Common_Hand_Rank.CommonRanker(common_rank, this, other);
            }
        }

        public void Draw(Card card) => _cards.Add(card);
    }
}

namespace Poker
{
    /*
    scorer for five cards poker.
    */
    public static class Scorer
    {
        /*
        Every function that we use to rank a hand.
        */
        public static Card HighCard(IEnumerable<Card> cards) => cards.Aggregate((highCard, nextCard) => nextCard.Value > highCard.Value ? nextCard : highCard);
        private static bool HasColor(IEnumerable<Card> cards) => cards.All(c => cards.First().Suit == c.Suit);
        private static bool HasRoyalColor(IEnumerable<Card> cards) => HasEscaleraColor(cards) && cards.All(c => c.Value > CardValue.Nueve);
        private static bool HasOfAKind(IEnumerable<Card> cards, int num) => cards.ToKindAndQuantities().Any(c => c.Value >= num);
        private static int CountOfAKind(IEnumerable<Card> cards, int num) => cards.ToKindAndQuantities().Count(c => c.Value == num);
        private static bool HasPair(IEnumerable<Card> cards) => HasOfAKind(cards, 2);
        private static bool HasTwoPair(IEnumerable<Card> cards) => CountOfAKind(cards, 2) == 2;
        private static bool HasThreeOfAKind(IEnumerable<Card> cards) => HasOfAKind(cards, 3);
        private static bool HasFourOfAKind(IEnumerable<Card> cards) => HasOfAKind(cards, 4);
        private static bool HasFullHouse(IEnumerable<Card> cards)
        {
            if (HasThreeOfAKind(cards))
            {
                return cards
                .GroupBy(x => x.Value)
                .Where(x => x.Count() == 2).Count() == 1;
            }
            return false;
        }  
        private static bool HasEscalera(IEnumerable<Card> cards)
        {
            if (cards.Any(x => x.Value == CardValue.As))
            {
                IEnumerable<int> c =  cards.Select(x => (   ((int)x.Value <= 5) ? (int)x.Value + 13 : (int)x.Value)  );
                return c.Order().SelectConsecutive((n, next) => (n + 1) == next).All(boolean => boolean);
            }
            return cards.OrderBy(card => card.Value).SelectConsecutive((n, next) => (n.Value + 1) == next.Value).All(boolean => boolean);
        }
        private static bool HasEscaleraColor(IEnumerable<Card> cards) => HasEscalera(cards) && HasColor(cards);

        // A list of ranks gives added flexibility to how hand ranks can be scored.
        // Each ranker has an Eval delegate that returns a bool

        // a function that gives us the rank of a hand, by taking an IEnumerable of cards. But first it takes a list of ranking functions and
        // ranks them by its priority, and after that we find the first function between the rankings, that evaluates true.
        public static HandRank GetHandRank(IEnumerable<Card> cards) => Rankings()
                           .OrderByDescending(card => card.rank)
                           .First(rule => rule.eval(cards)).rank;

        /*
        This contains the rankings in tuples, of the form
        ( Delegate, rank of hand). 

        functions as data.
        */
        private static List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)> Rankings() =>
           new List<(Func<IEnumerable<Card>, bool> eval, HandRank rank)>
           {
                       (cards => HasRoyalColor(cards), HandRank.EscaleraReal),
                       (cards => HasEscaleraColor(cards), HandRank.EscaleraColor),
                       (cards => HasFourOfAKind(cards), HandRank.CuatroIguales),
                       (cards => HasFullHouse(cards), HandRank.Full),
                       (cards => HasColor(cards), HandRank.Color),
                       (cards => HasEscalera(cards), HandRank.Escalera),
                       (cards => HasThreeOfAKind(cards), HandRank.Trio),
                       (cards => HasTwoPair(cards), HandRank.DosParejas),
                       (cards => HasPair(cards), HandRank.Pareja),
                       (cards => true, HandRank.CartaAlta),
           };
    }
}
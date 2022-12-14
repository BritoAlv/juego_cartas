using FluentAssertions;
using Xunit;

namespace Poker
{
    public class HandScorerTest
    {
        [Fact]
        public void CanScoreHand()
        {
            var hand = new Hand();
            hand.Draw(new Card(CardValue.As, CardSuit.Pica));
            hand.Draw(new Card(CardValue.Príncipe, CardSuit.Diamante));
            hand.Draw(new Card(CardValue.Reina, CardSuit.CorazónRojo));
            hand.Draw(new Card(CardValue.Rey, CardSuit.CorazónRojo));
            hand.Draw(new Card(CardValue.Dos, CardSuit.CorazónRojo));
            Scorer.GetHandRank(hand.Cards).Should().Be(HandRank.Escalera);
        }
    }
}

using FluentAssertions;
using Poker;
using Xunit;

public class HandScorerTest
{
    [Fact]
    public void CanScoreHand()
    {
        Scorer scorer = new Scorer();
        var hand = new Hand(scorer);
        hand.Draw(new Card(CardValue.As, CardSuit.Pica));
        hand.Draw(new Card(CardValue.Príncipe, CardSuit.Diamante));
        hand.Draw(new Card(CardValue.Reina, CardSuit.CorazónRojo));
        hand.Draw(new Card(CardValue.Rey, CardSuit.CorazónRojo));
        hand.Draw(new Card(CardValue.Dos, CardSuit.CorazónRojo));
        scorer.GetHandRank(hand.Cards).Id.Should().Be("El2");
    }
}

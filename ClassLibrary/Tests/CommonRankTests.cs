using FluentAssertions;
using Xunit;
using Poker;

public class CommonRankTests
{
    [Fact]
    public void CanRankAFull()
    {
        Scorer scorer = new Scorer();
        var A = new Hand(scorer);
        A.Draw(new Card(CardValue.Siete, CardSuit.Pica));
        A.Draw(new Card(CardValue.Siete, CardSuit.Diamante));
        A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));

        var B = new Hand(scorer);
        B.Draw(new Card(CardValue.Seis, CardSuit.Pica));
        B.Draw(new Card(CardValue.Seis, CardSuit.Diamante));
        B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));

        A.CompareTo(B).Should().Be(1);
    }

    [Fact]
    public void CanRankATrio()
    {
        Scorer scorer = new Scorer();
        var A = new Hand(scorer);
        A.Draw(new Card(CardValue.Diez, CardSuit.Pica));
        A.Draw(new Card(CardValue.Siete, CardSuit.Diamante));
        A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        var B = new Hand(scorer);
        B.Draw(new Card(CardValue.Cinco, CardSuit.Pica));
        B.Draw(new Card(CardValue.Seis, CardSuit.Diamante));
        B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
        A.CompareTo(B).Should().Be(1);
    }
}
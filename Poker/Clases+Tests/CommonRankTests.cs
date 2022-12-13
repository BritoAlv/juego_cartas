using FluentAssertions;
using Xunit;

namespace Poker
{
    public class CommonRankTests
    {
        [Fact]
        public void CanRankAFull()
        {
            var A = new Hand();
            A.Draw(new Card(CardValue.Siete, CardSuit.Pica));
            A.Draw(new Card(CardValue.Siete, CardSuit.Diamante));
            A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
            A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
            A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));

            var B = new Hand();
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
            var A = new Hand();
            A.Draw(new Card(CardValue.Diez, CardSuit.Pica));
            A.Draw(new Card(CardValue.Siete, CardSuit.Diamante));
            A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
            A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
            A.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));

            var B = new Hand();
            B.Draw(new Card(CardValue.Cinco, CardSuit.Pica));
            B.Draw(new Card(CardValue.Seis, CardSuit.Diamante));
            B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
            B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));
            B.Draw(new Card(CardValue.Diez, CardSuit.CorazónRojo));

            A.CompareTo(B).Should().Be(1);
        }
    }
}
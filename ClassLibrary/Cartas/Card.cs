using System.Diagnostics.CodeAnalysis;

namespace Poker;
public partial class Card : ICloneable, IDescribable<Card>, IEqualityComparer<Card>
{
    public Card(CardValue value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }
    public CardValue Value { get; private set; }
    
    public CardSuit Suit { get; }
    public object Clone()
    {
        return new Card(this.Value, this.Suit);
    }

    public bool Equals(Card? x, Card? y)
    {
        return this.Iguales(y);
    }

    public int GetHashCode([DisallowNull] Card obj)
    {
        return obj.Suit.GetHashCode() + obj.Value.GetHashCode();
    }

    public bool Iguales(object? obj)
    {
        if (obj is Card other)
        {
            return this.Suit == other.Suit && this.Value == other.Value;
        }
        return false;
    }
    public override string ToString()
    {
        int numero = ((int)this.Value);
        if (numero == 14)
        {
            numero = 1;
        }
        string number = numero.ToString();
        string suit = this.Suit.GetEmoji().ToString();
        if (number.Length == 1)
        {
            number = " " + number;
        }
        return number + suit + "   ";
    }

    public int get_value()
    {
        if (this.Value == CardValue.As)
        {
            return 1;
        }
        return (int)this.Value;
    }

    public void change_value(int a)
    {
        if (a > 1)
        {
            this.Value = (CardValue)a;
        }
    }

}

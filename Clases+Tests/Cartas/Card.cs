namespace Poker;
public class Card
{
    public Card(CardValue value, CardSuit suit)
    {
        Value = value;
        Suit = suit;
    }
    public CardValue Value { get; }
    public CardSuit Suit { get; }
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
}

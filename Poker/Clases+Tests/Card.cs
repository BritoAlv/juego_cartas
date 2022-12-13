using NeoSmart.Unicode;
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
        string number = ((int)this.Value).ToString();
        string suit = this.Suit.GetEmoji().ToString();
        if(number.Length == 1)
        {
            number = " " + number;
        }
        return number + suit + "   ";
    } 
}
  
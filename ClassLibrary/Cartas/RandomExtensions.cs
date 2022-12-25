namespace Poker;
public static class Random_Utils
{
    public static T NextEnum<T>(this Random random)
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(random.Next(values.Length))!;
    }
    public static Hand generate_random_hand(Scorer scorer)
    {
        Hand A = new Hand(scorer);
        for (int i = 0; i < 5; i++)
        {
            A.Draw(generate_random_card());
        }
        return A;
    }
    public static Card generate_random_card()
    {
        return new Card(generate_random_value(), generate_random_suit());
    }
    public static CardSuit generate_random_suit()
    {
        var rnd = new Random();
        return rnd.NextEnum<CardSuit>();
    }
    public static CardValue generate_random_value()
    {
        var rnd = new Random();
        return rnd.NextEnum<CardValue>();
    }
}
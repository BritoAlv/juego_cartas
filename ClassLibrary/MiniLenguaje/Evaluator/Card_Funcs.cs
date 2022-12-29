namespace Poker;
public partial class Func_Generator
{
    private Func<IEnumerable<Card>, IEnumerable<Card?>> Card_Func_Suit(string text)
    {
        switch (text)
        {
            case "corazonrojo":
                return x => x.Where(m => m.Suit == CardSuit.CorazónRojo);
            case "diamante":
                return x => x.Where(m => m.Suit == CardSuit.Diamante);
            case "pica":
                return x => x.Where(m => m.Suit == CardSuit.Pica);
            case "trebol":
                return x => x.Where(m => m.Suit == CardSuit.Pica);
        }
        return x => Enumerable.Empty<Card?>();
    }
    private Func<IEnumerable<Card>, IEnumerable<Card?>> Card_Func_Valor(string text)
    {
        if (text.StartsWith(">"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(x => x.get_value() > a);
        }
        if (text.StartsWith("<"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(x => x.get_value() < a);
        }
        if (text == "mayor")
        {
            return x => x.OrderByDescending(x => x.Value);
        }
        if (text == "menor")
        {
            return x => x.OrderBy(x => x.Value);
        }
        if (int.TryParse(text, out var val))
        {
            return x => x.Where(m => m.get_value() == val);
        }
        return x => Enumerable.Empty<Card?>();
    }
}
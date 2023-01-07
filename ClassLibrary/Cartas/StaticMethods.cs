namespace Poker;
public partial class Card : ICloneable, IDescribable<Card>, IEqualityComparer<Card>
{
    public static string Valor => "Carta";
    private static Func<IEnumerable<Card>, IEnumerable<Card>> Card_Func_Suit(string text)
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
                return x => x.Where(m => m.Suit == CardSuit.Trébol);
        }
        return x => Enumerable.Empty<Card>();
    }
    private static Func<IEnumerable<Card>, IEnumerable<Card>> Card_Func_Valor(string text)
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
            return x => x.OrderByDescending(x => x.Value).Take(1);
        }
        if (text == "menor")
        {
            return x => x.OrderBy(x => x.Value).Take(1);
        }
        if (int.TryParse(text, out var val))
        {
            return x => x.Where(m => m.get_value() == val);
        }
        return x => Enumerable.Empty<Card>();
    }
    public static Func<IEnumerable<Card>, IEnumerable<Card>> get_T_func(UnaryDescriptionArgument unary)
    {
        var identifier = unary.Objeto.Text;
        switch (identifier)
        {
            case "Valor":
                return Card_Func_Valor(unary.Description.Text);
            case "Suit":
                return Card_Func_Suit(unary.Description.Text);
            default:
                return x => new List<Card>();
        }
    }
}
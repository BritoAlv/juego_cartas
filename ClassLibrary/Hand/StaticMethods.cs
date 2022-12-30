namespace Poker;

public partial class Hand : IComparable<Hand>, IDescribable<Hand>, IEqualityComparer<Hand>
{
    private static Func<IEnumerable<Hand>, IEnumerable<Hand>> Hand_Func_Rank(string text)
    {
        return x => x.Where(x => x.rank.Id == text);
    }
    private static Func<IEnumerable<Hand>, IEnumerable<Hand>> Hand_Func_Valor(string text)
    {
        if (text.StartsWith(">"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(x => x.rank.Priority > a);
        }
        if (text.StartsWith("<"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(x => x.rank.Priority < a);
        }
        if (text == "mayor")
        {
            return x => x.OrderByDescending(x => x.rank);
        }
        if (text == "menor")
        {
            return x => x.OrderBy(x => x.rank);
        }
        return x => Enumerable.Empty<Hand>();
    }

    public static Func<IEnumerable<Hand>, IEnumerable<Hand>> get_T_func(UnaryDescriptionArgument unary)
    {
        var identifier = unary.Objeto.Text;
        switch (identifier)
        {
            case "Valor":
                return Hand_Func_Valor(unary.Description.Text);
            case "Rank":
                return Hand_Func_Rank(unary.Description.Text);
            default:
                return x => new List<Hand>();
        }
    }
}
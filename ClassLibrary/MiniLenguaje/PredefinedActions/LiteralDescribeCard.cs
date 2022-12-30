namespace Poker;

public class LiteralDescribeCard : IArgument<Card>
{
    public LiteralDescribeCard(Token open_brace, LiteralArguments literalArguments, Token closed_brace)
    {
        Open_brace = open_brace;
        LiteralArguments = literalArguments;
        Closed_brace = closed_brace;
    }

    public string valor => "Literal Describe Card: ";
    public Token Open_brace { get; }
    public LiteralArguments LiteralArguments { get; }
    public Token Closed_brace { get; }
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_brace;
        yield return LiteralArguments;
        yield return Closed_brace;
    }
    public Card Get_Object(IEnumerable<Card> list, IGlobal_Contexto contexto)
    {
        foreach (var Func in GetCardFunction(LiteralArguments))
        {
            IEnumerable<Card> obtained_card = Func(list);
            if (obtained_card.Count() > 0)
            {
                return obtained_card.First();
            }
        }
        throw new Exception("No se encontró la carta");
    }

    private Func<IEnumerable<Card>, IEnumerable<Card>> Card_Func_Suit(string text)
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
        return x => Enumerable.Empty<Card>();
    }
    private Func<IEnumerable<Card>, IEnumerable<Card>> Card_Func_Valor(string text)
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
        return x => Enumerable.Empty<Card>();
    }

    private List<Func<IEnumerable<Card>, IEnumerable<Card>>> GetCardFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<Card>, IEnumerable<Card>>> result = new List<Func<IEnumerable<Card>, IEnumerable<Card>>>();
        foreach (var argument in arguments.Descriptions)
        {
            if (argument is UnaryDescriptionArgument unary)
            {
                result.Add(get_card_func(unary));
            }
            else if (argument is BinaryDescriptionArgument binary)
            {
                result.Add(get_card_func(binary));
            }
        }
        return result;
    }

    private Func<IEnumerable<Card>, IEnumerable<Card>> get_card_func(UnaryDescriptionArgument unary)
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
    private Func<IEnumerable<Card>, IEnumerable<Card>> get_card_func(BinaryDescriptionArgument binary)
    {
        if (binary.Operador.Text == "&&")
        {
            Func<IEnumerable<Card>, IEnumerable<Card>> card1 = get_card_func(binary.Izq);
            Func<IEnumerable<Card>, IEnumerable<Card>> card2 = get_card_func(binary.Der);
            return x => card1(x).Intersectt(card2(x));
        }
        else if (binary.Operador.Text == "||")
        {
            Func<IEnumerable<Card>, IEnumerable<Card>> card1 = get_card_func(binary.Izq);
            Func<IEnumerable<Card>, IEnumerable<Card>> card2 = get_card_func(binary.Der);
            return x => card1(x).Unionn(card2(x));
        }
        return x => new List<Card>();
    }
}
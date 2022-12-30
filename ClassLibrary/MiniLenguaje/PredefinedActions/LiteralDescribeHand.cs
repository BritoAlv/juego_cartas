namespace Poker;

public class LiteralDescribeHand : IArgument<Hand>
{
    private readonly Token open_Question;

    public LiteralDescribeHand(Token open_question, LiteralArguments literalArguments, Token closed_question)
    {
        open_Question = open_question;
        LiteralArguments = literalArguments;
        Closed_Question = closed_question;
    }

    public string valor => "Literal Describe Hand: ";
    public LiteralArguments LiteralArguments { get; }
    public Token Closed_Question { get; }

    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return open_Question;
        yield return LiteralArguments;
        yield return Closed_Question;
    }
    public Hand Get_Object(IEnumerable<Hand> list, IGlobal_Contexto contexto)
    {
        foreach (var Func in GetHandFunction(LiteralArguments))
        {
            IEnumerable<Hand> obtained_hand = Func(list);
            if (obtained_hand.Count() > 0)
            {
                return obtained_hand.First();
            }
        }
        throw new Exception("No se encontró la mano");
    }

    private Func<IEnumerable<Hand>, IEnumerable<Hand>> Hand_Func_Rank(string text)
    {
        return x => x.Where(x => x.rank.Id == text);
    }
    private Func<IEnumerable<Hand>, IEnumerable<Hand>> Hand_Func_Valor(string text)
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

    private List<Func<IEnumerable<Hand>, IEnumerable<Hand>>> GetHandFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<Hand>, IEnumerable<Hand>>> result = new List<Func<IEnumerable<Hand>, IEnumerable<Hand>>>();
        foreach (var argument in arguments.Descriptions)
        {
            if (argument is UnaryDescriptionArgument unary)
            {
                result.Add(get_hand_func(unary));
            }
            else if (argument is BinaryDescriptionArgument binary)
            {
                result.Add(get_hand_func(binary));
            }
        }
        return result;
    }

    private Func<IEnumerable<Hand>, IEnumerable<Hand>> get_hand_func(UnaryDescriptionArgument unary)
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
    private Func<IEnumerable<Hand>, IEnumerable<Hand>> get_hand_func(BinaryDescriptionArgument binary)
    {
        if (binary.Operador.Text == "&&")
        {
            Func<IEnumerable<Hand>, IEnumerable<Hand>> hand1 = get_hand_func(binary.Izq);
            Func<IEnumerable<Hand>, IEnumerable<Hand>> hand2 = get_hand_func(binary.Der);
            return x => hand1(x).Intersectt(hand2(x));
        }
        else if (binary.Operador.Text == "||")
        {
            Func<IEnumerable<Hand>, IEnumerable<Hand>> hand1 = get_hand_func(binary.Izq);
            Func<IEnumerable<Hand>, IEnumerable<Hand>> hand2 = get_hand_func(binary.Der);
            return x => hand2(x).Unionn(hand2(x));
        }
        return x => new List<Hand>();
    }
}
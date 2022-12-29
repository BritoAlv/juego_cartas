namespace Poker;
public partial class Func_Generator
{
    public IGlobal_Contexto Contexto { get; }
    public Func_Generator(IGlobal_Contexto contexto)
    {
        Contexto = contexto;
    }
    public List<Func<IEnumerable<Card>, IEnumerable<Card?>>> GetCardFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<Card>, IEnumerable<Card?>>> result = new List<Func<IEnumerable<Card>, IEnumerable<Card?>>>();
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
    private Func<IEnumerable<Card>, IEnumerable<Card?>> get_card_func(BinaryDescriptionArgument binary)
    {
        if (binary.Operador.Text == "&&")
        {
            Func<IEnumerable<Card>, IEnumerable<Card?>> card1 = get_card_func(binary.Izq);
            Func<IEnumerable<Card>, IEnumerable<Card?>> card2 = get_card_func(binary.Der);
            return x => card1(x).Intersect(card2(x));

        }
        else if (binary.Operador.Text == "||")
        {
            Func<IEnumerable<Card>, IEnumerable<Card?>> card1 = get_card_func(binary.Izq);
            Func<IEnumerable<Card>, IEnumerable<Card?>> card2 = get_card_func(binary.Der);
            return x => card1(x).Union(card2(x));
        }
        return x => new List<Card?>();
    }
    internal List<Func<IEnumerable<Player>, IEnumerable<Player?>>> GetPlayerFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<Player>, IEnumerable<Player?>>> result = new List<Func<IEnumerable<Player>, IEnumerable<Player?>>>();
        foreach (var argument in arguments.Descriptions)
        {
            if (argument is UnaryDescriptionArgument unary)
            {
                result.Add(get_player_func(unary));
            }
            else if (argument is BinaryDescriptionArgument binary)
            {
                result.Add(get_player_func(binary));
            }
        }
        return result;
    }
    private Func<IEnumerable<Player>, IEnumerable<Player?>> get_player_func(BinaryDescriptionArgument binary)
    {
        if (binary.Operador.Text == "&&")
        {
            Func<IEnumerable<Player>, IEnumerable<Player?>> player1 = get_player_func(binary.Izq);
            Func<IEnumerable<Player>, IEnumerable<Player?>> player2 = get_player_func(binary.Der);
            return x => player1(x).Intersect(player2(x));

        }
        else if (binary.Operador.Text == "||")
        {
            Func<IEnumerable<Player>, IEnumerable<Player?>> player1 = get_player_func(binary.Izq);
            Func<IEnumerable<Player>, IEnumerable<Player?>> player2 = get_player_func(binary.Der);
            return x => player1(x).Union(player2(x));
        }
        return x => Enumerable.Empty<Player?>();
    }
}
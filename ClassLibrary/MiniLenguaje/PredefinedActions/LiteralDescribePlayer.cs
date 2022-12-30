namespace Poker;
public class LiteralDescribePlayer : IArgument<Player>
{
    public LiteralDescribePlayer(Token open_llave, LiteralArguments literalArguments, Token closed_llave)
    {
        Open_llave = open_llave;
        LiteralArguments = literalArguments;
        Closed_llave = closed_llave;
    }
    public Token Open_llave { get; }
    public LiteralArguments LiteralArguments { get; }
    public Token Closed_llave { get; }
    public string valor => "Literal Describe Player";
    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_llave;
        yield return LiteralArguments;
        yield return Closed_llave;
    }
    public Player Get_Object(IEnumerable<Player> list, IGlobal_Contexto contexto)
    {
        foreach (var Func in GetPlayerFunction(LiteralArguments))
        {
            IEnumerable<Player> obtained_player = Func(list);
            if (obtained_player.Count() > 0)
            {
                return obtained_player.First();
            }
        }
        throw new Exception("No se encontró el jugador");
    }

    private Func<IEnumerable<Player>, IEnumerable<Player>> get_player_func(UnaryDescriptionArgument unary)
    {
        var identifier = unary.Objeto.Text;
        switch (identifier)
        {
            case "Jugador":
                return Player_Func_Jugador(unary.Description.Text);
            case "Dinero":
                return Player_Func_Dinero(unary.Description.Text);
            case "Apuesta":
                return Player_Func_Apuesta(unary.Description.Text);
            default:
                return x => new List<Player>();
        }
    }

    private Func<IEnumerable<Player>, IEnumerable<Player>> Player_Func_Jugador(string text)
    {
        return x => x.Where(m => m.Id == text);
    }
    private Func<IEnumerable<Player>, IEnumerable<Player>> Player_Func_Dinero(string text)
    {
        if (text == "mayor")
        {
            return x => x.OrderByDescending(m => m.Dinero);
        }

        else if (text.StartsWith(">"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(m => m.Dinero > a);
        }

        else if (text == "menor")
        {
            return x => x.OrderBy(m => m.Dinero);
        }

        else if (text.StartsWith("<"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(m => m.Dinero < a);
        }
        return x => Enumerable.Empty<Player>();
    }

    private Func<IEnumerable<Player>, IEnumerable<Player>> Player_Func_Apuesta(string text)
    {
        if (text == "mayorapostador")
        {
            return x => x.OrderByDescending(x => x.Apuestas.Summ());
        }
        if (text == "mayor")
        {
            return x => x.OrderByDescending(x => x.Apuestas.Maxx());
        }
        if (text == "menorapostador")
        {
            return x => x.OrderBy(x => x.Apuestas.Summ());
        }
        if (text == "menor")
        {
            return x => x.OrderBy(x => x.Apuestas.Maxx());
        }
        return x => Enumerable.Empty<Player>();
    }



    internal List<Func<IEnumerable<Player>, IEnumerable<Player>>> GetPlayerFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<Player>, IEnumerable<Player>>> result = new List<Func<IEnumerable<Player>, IEnumerable<Player>>>();
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

    

    private Func<IEnumerable<Player>, IEnumerable<Player>> get_player_func(BinaryDescriptionArgument binary)
    {
        if (binary.Operador.Text == "&&")
        {
            Func<IEnumerable<Player>, IEnumerable<Player>> player1 = get_player_func(binary.Izq);
            Func<IEnumerable<Player>, IEnumerable<Player>> player2 = get_player_func(binary.Der);
            return x => player1(x).Intersectt(player2(x));

        }
        else if (binary.Operador.Text == "||")
        {
            Func<IEnumerable<Player>, IEnumerable<Player>> player1 = get_player_func(binary.Izq);
            Func<IEnumerable<Player>, IEnumerable<Player>> player2 = get_player_func(binary.Der);
            return x => player1(x).Unionn(player2(x));
        }
        return x => Enumerable.Empty<Player>();
    }
}

namespace Poker;
public abstract partial class Player : Ideable, IApostador, IDescribable<Player>, IEqualityComparer<Player>
{
    public static string Valor => "Jugador";
    public static Func<IEnumerable<Player>, IEnumerable<Player>> get_T_func(UnaryDescriptionArgument unary)
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
            case "Mano":
                return Player_Func_Mano(unary.Description.Text);
            default:
                return x => new List<Player>();
        }
    }
    private static Func<IEnumerable<Player>, IEnumerable<Player>> Player_Func_Mano(string text)
    {
        if (text == "mayor")
        {
            return x => x.OrderByDescending(m => m.Hand);
        }
        else if (text.StartsWith(">"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(m => m.Hand.rank.Priority > a);
        }
        else if (text == "menor")
        {
            return x => x.OrderBy(m => m.Hand);
        }
        else if (text.StartsWith("<"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(m => m.Hand.rank.Priority < a);
        }
        return x => x.Where(m => m.Id == text);
    }

    private static Func<IEnumerable<Player>, IEnumerable<Player>> Player_Func_Jugador(string text)
    {
        if (text == "random")
        {
            return x => GetRandom_Player(x.ToList());
        }
        return x => x.Where(m => m.Id == text);
    }

    private static IEnumerable<Player> GetRandom_Player(List<Player> players)
    {
        Random a = new Random();
        int index = a.Next(0, players.Count);
        Player player = players[index];
        players[index] = players[0];
        players[0] = player;
        return players;

    }

    private static Func<IEnumerable<Player>, IEnumerable<Player>> Player_Func_Dinero(string text)
    {
        if (text == "mayor")
        {
            return x => x.OrderByDescending(m => m.Dinero).Take(1);
        }

        else if (text.StartsWith(">"))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(m => m.Dinero > a);
        }

        else if (text.StartsWith("="))
        {
            int a = int.Parse(text.Substring(1));
            return x => x.Where(m => m.Dinero == a);
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

    private static Func<IEnumerable<Player>, IEnumerable<Player>> Player_Func_Apuesta(string text)
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
}
namespace Poker;
public partial class Func_Generator
{
    private Func<IEnumerable<Player>, IEnumerable<Player?>> Player_Func_Jugador(string text)
    {
        return x => x.Where(m => m.Id == text);
    }
    private Func<IEnumerable<Player>, IEnumerable<Player?>> Player_Func_Dinero(string text)
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
        return x => Enumerable.Empty<Player?>();
    }

    private Func<IEnumerable<Player>, IEnumerable<Player?>> Player_Func_Apuesta(string text)
    {
        if (text == "mayorapostador")
        {
            return x => x.OrderByDescending(x => x.Apuestas.Sum());
        }
        if (text == "mayor")
        {
            return x => x.OrderByDescending(x => x.Apuestas.Max());
        }
        if (text == "menorapostador")
        {
            return x => x.OrderBy(x => x.Apuestas.Sum());
        }
        if (text == "menor")
        {
            return x => x.OrderBy(x => x.Apuestas.Max());
        }
        return x => Enumerable.Empty<Player?>();
    }
}
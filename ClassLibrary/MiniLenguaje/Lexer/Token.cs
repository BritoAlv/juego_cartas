namespace Poker;

public class Token : Iprintable
{
    public Token(Tipo tipo, string text)
    {
        Tipo = tipo;
        Text = text;
    }

    public Tipo Tipo { get; }
    public string Text{ get; }
    public string valor => Text;

    public IEnumerable<Iprintable> GetChildrenIprintables()
    {
        return Enumerable.Empty<Iprintable>();
    }
}

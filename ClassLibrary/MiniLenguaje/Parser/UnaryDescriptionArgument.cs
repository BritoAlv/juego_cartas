namespace Poker;

public class UnaryDescriptionArgument : DescriptionArgument
{
    public UnaryDescriptionArgument(Token objeto, Token description)
    {
        Objeto = objeto;
        Description = description;
    }
    public override string valor => $"Unary Argument Description";
    public Token Objeto { get; }
    public Token Description { get; }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return new Token(Tipo.Wrong, $"{Objeto.Text} : {Description.Text}");
    }
}

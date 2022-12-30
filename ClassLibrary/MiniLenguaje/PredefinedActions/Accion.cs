namespace Poker;

public abstract class Accion : Iprintable
{
    public Accion(Token open_parenthesis, Token signature, Token closed_parenthesis)
    {
        Open_Parenthesis = open_parenthesis;
        Signature = signature;
        Closed_Parenthesis = closed_parenthesis;
    }
    public Token Open_Parenthesis { get; }
    public Token Signature { get; }
    public Token Closed_Parenthesis { get; }
    public string valor => "Acción " + Signature.Text;
    public abstract IEnumerable<Iprintable> GetChildrenIprintables();
}

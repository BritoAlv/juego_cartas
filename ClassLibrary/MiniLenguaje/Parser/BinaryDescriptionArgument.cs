namespace Poker;

public class BinaryDescriptionArgument: DescriptionArgument
{
    public BinaryDescriptionArgument(UnaryDescriptionArgument izq, Token operador, UnaryDescriptionArgument der)
    {
        Izq = izq;
        Operador = operador;
        Der = der;
    }
    public override string valor => "Binary Argument Description";
    public UnaryDescriptionArgument Izq { get; }
    public Token Operador { get; }
    public UnaryDescriptionArgument Der { get; }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Izq;
        yield return Operador;
        yield return Der;
    }
}
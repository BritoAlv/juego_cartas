namespace Poker;

/// <summary>
/// Only one place to declare predefined actions.
/// </summary>
public static class Factory
{
    // calls to the factory should have Current = )
    public static object CreateAction(string v, Parser parser)
    {
        var open_parenthesis = parser.Match(Tipo.ParéntesisAbierto);
        var signature = parser.Match(Tipo.Accion);
        switch (v)
        {
            case "$añadircarta":
                return new AñadirCarta(open_parenthesis, signature, parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado));
            case "$robarcarta":
                return new RobarCarta(open_parenthesis, signature, parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado));
            case "$banearjugador":
                return new BanearJugador(open_parenthesis, signature, parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado));
            default:
                throw new Exception("La acción no está predefinida");
        }
    }
}
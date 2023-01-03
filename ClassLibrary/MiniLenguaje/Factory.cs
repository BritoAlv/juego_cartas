namespace Poker;

/// <summary>
/// Only one place to declare predefined actions.
/// </summary>
public class Factory
{
    public Factory()
    {
        this.predefined_actions = new List<Func<string, Parser, object?>>();
        predefined_actions.Add
        (
            (x, parser) => x == "$añadircarta" ? new AñadirCarta(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$robarcarta" ? new RobarCarta(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Card>(), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
        predefined_actions.Add
        (
            (x, parser) => x == "$banearjugador" ? new BanearJugador(parser.Match(Tipo.ParéntesisAbierto), parser.Match(Tipo.Accion), parser.ParseArgument<Player>(), parser.Match(Tipo.ParéntesisCerrado)) : null
        );
    }

    public List<Func<string, Parser, object?>> predefined_actions { get; private set; }

    internal object CreateAction(string text, Parser parser)
    {
        foreach (var func in predefined_actions)
        {
            var result = func(text, parser);
            if (result != null)
            {
                return result;
            }
        }
        throw new Exception("No se encontró la acción");

    }

    // calls to the factory should have Current = ) parser responsibility.

    public void AddPredefined(Func<string, Parser, object> func)
    {
        predefined_actions.Add(func);
    }
}
namespace Poker;

/// <summary>
/// Only one place to declare predefined actions.
/// </summary>
public class Factory
{
    public Factory()
    {
        this.predefined_actions = new List<Func<string, Parser, object?>>();
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
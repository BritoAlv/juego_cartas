using AnálisisCodigo;
using AnálisisCodigo.Sintaxis;
public static class L
{
    public static object Use_Compiler()
    {
        var variables = new Dictionary<VariableSymbol, object>();
        var line = Console.ReadLine();
        var syntaxtree = NodoRoot.Parse(line);
        var compilacion = new Compilacion(syntaxtree);
        var result = compilacion.Evaluate(variables);
        return result.Value;
    }
}


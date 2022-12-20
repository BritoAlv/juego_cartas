using AnálisisCodigo;
using AnálisisCodigo.Sintaxis;
namespace LenguajeAPI;
public static class API
{
    public static object Use_Compiler(string line)
    {
        var variables = new Dictionary<VariableSymbol, object>();
        var syntaxtree = NodoRoot.Parse(line);
        var compilacion = new Compilacion(syntaxtree);
        var result = compilacion.Evaluate(variables);
        return result.Value;
    }
}


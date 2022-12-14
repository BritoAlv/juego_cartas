using AnálisisCodigo;
using AnálisisCodigo.Sintaxis;
public class Test
{
    public static void Main()
    {
        var variables = new Dictionary<VariableSymbol, object>();
        var showTree = false;
        while (true)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }
            else if (line == "#clear")
            {
                Console.Clear();
                continue;
            }

            var syntaxtree = NodoRoot.Parse(line);
            var compilacion = new Compilacion(syntaxtree);
            var result = compilacion.Evaluate(variables);
            var diagnostics = result.Diagnostics;
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            print_tree.print(syntaxtree.Root);
            Console.ForegroundColor = color;
            if (!diagnostics.Any())
            {
                Console.WriteLine(result.Value);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                foreach (var diag in diagnostics)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(diag);
                    Console.ResetColor();

                    var prefix = line.Substring(0, diag.Span.Start);
                    var error = line.Substring(diag.Span.Start, diag.Span.Length);
                    var suffix = line.Substring(diag.Span.End);

                    Console.Write("    ");
                    Console.Write(prefix);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(error);
                    Console.ResetColor();
                    Console.Write(suffix);
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
        }
    }
}

using System.Text;
using AnálisisCodigo;
using AnálisisCodigo.Sintaxis;
public class Test
{
    public static void Main()
    {
        var variables = new Dictionary<VariableSymbol, object>();
        var textBuilder = new StringBuilder();
        while (true)
        {
            if (textBuilder.Length == 0)
            {
                Console.Write("> ");
            }
            else
            {
                Console.Write("| ");
            }

            var input = Console.ReadLine();
            var isBlank = string.IsNullOrWhiteSpace(input);

            if (textBuilder.Length == 0)
            {
                if (isBlank)
                {
                    break;
                }
                else if (input == "#clear")
                {
                    Console.Clear();
                    continue;
                }
            }
            textBuilder.AppendLine(input);
            var text = textBuilder.ToString();
            var syntaxtree = NodoRoot.Parse(text);

            if (!isBlank && syntaxtree.Diagnostics.Any())
            {
                continue;
            }

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
                    Console.Write("    ");
                    Console.WriteLine();
                }
                Console.ResetColor();
            }
            textBuilder.Clear();
        }
    }
}

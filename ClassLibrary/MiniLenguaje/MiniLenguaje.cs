namespace Poker;
public class Mini_Lenguaje
{
    internal Mini_Lenguaje(Player A, IGlobal_Contexto contexto)
    {
        this.A = A;
        Contexto = contexto;
    }
    private Player A { get; }
    private IGlobal_Contexto Contexto { get; }
    public void Evaluate(string? line)
    {
        // check if user is on the clouds
        if (string.IsNullOrEmpty(line))
        {
            return;
        }
        // get tokens from the string.
        Lexer lexer = new Lexer(line);
        List<Token> tokens = lexer.Lex();
        Parser parser = new Parser(tokens);
        var tree = parser.Parse();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(line);
        Console.ResetColor();
        print_tree.print(tree);
        Evaluator evaluator = new Evaluator(tree, Contexto);
        evaluator.Evaluate();
    }
}


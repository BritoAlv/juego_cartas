namespace Poker;
public class Mini_Lenguaje
{
    internal Mini_Lenguaje(Player A, IGlobal_Contexto contexto)
    {
        Contexto = contexto;
    }
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
        var tree = (Return<bool>)parser.Parse();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(line);
        Console.ResetColor();
        print_tree.print(tree);
        try
        {
            tree.Get_Object(Enumerable.Empty<bool>(), Contexto);
            Console.WriteLine("El efecto se pudo realizar sin problemas");
        }
        catch (System.Exception)
        {
            Console.WriteLine("Hubo un problema con el efecto");
        }
        
    }
}


using Poker;
public class Program
{
    public static void Main()
    {
        var test_string = "( $añadircarta [ ( $robarcarta [Valor mayor && Suit corazón rojo] {Jugador ALVARO}) ]  {Bet mayor })";
        Lexer lexer = new Lexer(test_string);
        List<Token> tokens = lexer.Lex();
        Parser parser = new Parser(tokens);
        var tree = parser.Parse();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine(test_string);
        Console.ResetColor();
        print_tree.print(tree);
    }
}
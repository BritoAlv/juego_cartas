using Poker;
public class Program
{
    public static void Main()
    {
        Lexer lexer = new Lexer("( $añadircarta [ ( $robarcarta [Valor mayor && Suit corazón rojo] {Jugador ALVARO}) ]  {Bet mayor })");
        List<Token> tokens = lexer.Lex();
        
    }


}
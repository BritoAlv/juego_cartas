using Poker;
public class Test
{
    public static void RandomBasicPlay()
    {
        var scorer = new Scorer();
        while (true)
        {
            Hand A = Random_Utils.generate_random_hand(scorer);
            Hand B = Random_Utils.generate_random_hand(scorer);
            Console.WriteLine(A.ToStringWithRank());
            Console.WriteLine(B.ToStringWithRank());
            int result = A.CompareTo(B);
            switch (result)
            {
                case 1:
                    if (A.rank == B.rank)
                    {
                        Console.WriteLine($"Poseen el mismo rango {A.rank}, still A wins.");
                    }
                    else
                    {
                        Console.WriteLine($"A le gana a B porque {A.rank} es mejor que {B.rank}");
                    }

                    break;
                case -1:
                    if (A.rank == B.rank)
                    {
                        Console.WriteLine($"Poseen el mismo rango {A.rank}, still B wins.");
                    }
                    else
                    {
                        Console.WriteLine($"B le gana a A porque {B.rank} es mejor que {A.rank}");
                    }
                    break;
                default:
                    Console.WriteLine("=> Están empatados");
                    break;
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
    public static void TestingLexerLinear()
    {
        Lexer lexer = new Lexer("( $añadircarta [ ( $robarcarta [Valor mayor && Suit corazón rojo] {Jugador ALVARO}) ]  {Apuesta mayor })");
        List<Token> tokens = lexer.Lex();
        foreach (var token in tokens)
        {
            Console.WriteLine(("Tipo: " + token.Tipo.ToString().PadRight(20)) + ("=> " + token.Text));
        }
    }
}
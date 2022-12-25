using Poker;
public class Test
{
    public static void RandomComputerPlay()
    {
        while (true)
        {
        Scorer scorer = new Scorer();

        // Define PLayers
        Player A = new Human_Player("Alvaro", 120);
        Player B = new Human_Player("Miguel", 100);
        Player C = new Human_Player("PC", 500);

        // Define settings for the Mini_Rounds. Generate Random Cards by Default.
        List<Mini_Ronda_Contexto> mini_rondas_contexto = new List<Mini_Ronda_Contexto>(){
            new Mini_Ronda_Contexto(3),
            new Mini_Ronda_Contexto(1),
             new Mini_Ronda_Contexto(1),
        };

        // Define settings for the rounds. 
        Ronda_Context ronda = new Ronda_Context(mini_rondas_contexto);

        // Define settings for the game.

        Global_Contexto context = new Global_Contexto(ronda, A, B, C);

        Manager manager = new Manager(scorer , context);
        manager.SimulateGame();
        }
    }
    public static void RandomBasicPlay()
    {
        var scorer = new Scorer();
        while (true)
        {
            Hand A = random.generate_random_hand(scorer);
            Hand B = random.generate_random_hand(scorer);
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
}
using Poker;
public class Test
{
    public static void RandomComputerPlay()
    {
        while (true)
        {
            Scorer scorer = new Scorer();
            Player A = new Basic_Computer_Player("Alvaro", 100000);
            Player B = new Basic_Computer_Player("Miguel", 20023);
            Basic_Computer_Player C = new Basic_Computer_Player("PC", 50);
            Manager manager = new Manager(scorer, new int[] { 3, 1, 1 }, A, B, C);
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
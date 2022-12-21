using Poker;
public class Program
{
    public static void Main()
    {
        Scorer scorer = new Scorer();
        Human_Player A = new Human_Player("Alvaro", 100);
        Human_Player B = new Human_Player("Miguel", 200);
        Test_Player C = new Test_Player("PC", 50);
        Manager manager = new Manager(scorer, new int[] {3,1,1}, A, B, C);
        manager.SimulateGame();
    }
}
using Poker;
public class Program
{
    public static void Main()
    {
        // Use below method to test.
        // Test.RandomComputerPlay();
        Scorer scorer = new Scorer();
        Player A = new Human_Player("Alvaro", 120);
        Player B = new Test_Player("Miguel", 100);
        Player C = new Basic_Computer_Player("PC", 500);
        Manager manager = new Manager(scorer, new int[] { 2, 3, 1, 1 }, A, B, C);
        manager.SimulateGame();
    }
}
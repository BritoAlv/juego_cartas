using Poker;
public class Program
{
    public static void Main()
    {
        // Use below method to test.
        // Test.RandomComputerPlay();
        Scorer scorer = new Scorer();
        Player A = new Computer_Player("Alvaro", 500);
        Player B = new Test_Player("Miguel", 100);
        Player C = new Computer_Player("PC", 500);
        Manager manager = new Manager(scorer, new int[] { 3, 1, 1 }, A, B, C);
        manager.SimulateGame();
    }
}

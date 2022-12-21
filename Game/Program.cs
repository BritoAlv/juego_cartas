using Poker;
public class Program
{
    public static void Main()
    {
        Scorer scorer = new Scorer();
        Player A = new Test_Player("Alvaro", 1000);
        Player B = new Human_Player("Miguel", 1000);
        Player C = new Computer_Player("PC", 1000);
        Manager manager = new Manager(scorer, new int[] { 3, 1, 1 }, A, B, C);
        manager.SimulateGame();
    }
}

using Poker;
public class Program
{
    public static void Main()
    {
        Scorer scorer = new Scorer();
        Player A = new Human_Player("Alvaro", 100000);
        Player B = new Computer_Player("Miguel", 20023);
        Player C = new Computer_Player("PC", 50);
        Manager manager = new Manager(scorer, new int[] { 3, 1, 1 }, A, B, C);
        manager.SimulateGame();
    }
}

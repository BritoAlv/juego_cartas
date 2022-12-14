using Game;
public class Program
{
    public static void Main()
    {
        Human_Player A = new Human_Player("Alvaro", 100);
        Human_Player B = new Human_Player("Miguel", 200);
        Computer_Player C = new Computer_Player("PC", 50);
        Manager manager = new Manager(A,B,C);
        manager.SimulateGame();
    }
}
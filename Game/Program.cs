using Poker;
public class Program
{
    public static void Main()
    {
        // Use below method to test.
        // Test.RandomComputerPlay();
        // Configure Match.
        // Create Scorer
        Scorer scorer = new Scorer();
        // Define PLayers
        Player A = new Human_Player("Alvaro", 120);
        Player B = new Human_Player("Miguel", 100);
        Player C = new Human_Player("PC", 500);
        // Define settings for the Mini_Rounds. Generate Random Cards by Default.
        List<Mini_Ronda_Contexto> mini_rondas_contexto = new List<Mini_Ronda_Contexto>(){
            new Mini_Ronda_Contexto(2),
            new Mini_Ronda_Contexto(3, new RepartidorComun()),
             new Mini_Ronda_Contexto(1,  new RepartidorComun()),
             new Mini_Ronda_Contexto(1, new RepartidorComun()),
        };
        // Define settings for the rounds. 
        Ronda_Context ronda = new Ronda_Context(mini_rondas_contexto);
        // Define settings for the game.
        Global_Contexto context = new Global_Contexto(ronda, A, B, C);
        Manager manager = new Manager(scorer , context);
        manager.SimulateGame();
    }
}
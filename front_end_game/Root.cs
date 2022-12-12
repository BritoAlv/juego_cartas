using backend.Competence;
using backend.Exercises;
using backend.Gamee;
using backend.Muscles;
using backend.Players;
public class Root
{
    public static void Main()
    {
        Player A = new NormalPlayer("Alvaro", new Musculos(10));
        Player B = new NormalPlayer("Otro", new Musculos(20));
        Competencia C = new Competencia(new Plancha("Plancha", new Conditions(10)));
        Game juego = new Game(A, B, C);
        juego.SimulateGame();
    }
}

using backend.Competence;
using backend.Exercises;
using backend.Muscles;
using backend.Players;
namespace backend.Gamee;
public class Game
{
    public Game(Player a, Player b, Competencia c)
    {
        players = new Player[2];
        players[0] = a;
        players[1] = b;
        C = c;
        this.scorer = new Scorer(players, C);
    }

    public Player[] players;
    public Competencia C { get; }
    internal Scorer scorer { get; }
    public void SimulateGame()
    {
        while (C.StopGameCondition(players))
        {
            SimularRonda();
        }
        ShowGameFinalState();
    }

    private void ShowGameFinalState()
    {
        var winner = C.return_winner(scorer);
        Console.WriteLine("Ganó" + winner.Id);
    }

    private void SimularRonda()
    {
        foreach (var player in players)
        {
            SimularJugada(player);
            Console.WriteLine();
        }

    }

    private void SimularJugada(Player player)
    {
        Console.WriteLine(player.ToString());
        Console.WriteLine();
        C.ShowExcercises();
        var ejercicio = EscogerEjercicio();
        IntentarHacerEjercicio(player, ejercicio);
        AcabarJugada(player);
    }

    private void IntentarHacerEjercicio(Player player, Ejercicio ejercicio)
    {
        bool puede_hacer = ejercicio.Puede_Hacer(player, ejercicio.Conditions); // there may be extra conditions apart from the imposed ones by the exercise
        if (puede_hacer)
        {
            scorer.Apply(player, ejercicio);
            // the Muscles object has to change in correspondence to the exercise.
            ejercicio.Cambios(player);
            player.LogPlayerInfo();
        }
        else
        {
            NoPuedeHacerLog(ejercicio);
        }
    }

    private static void NoPuedeHacerLog(Ejercicio ejercicio)
    {
        Console.WriteLine("No tienes las condiciones necesarias para hacer este ejercicio");
        Console.WriteLine("Estas Son:");
        Console.WriteLine(ejercicio.Conditions.ToString());
    }

    private void AcabarJugada(Player player)
    {
        Console.WriteLine($"El turno de {player.Id} ha acabado");
    }

    private Ejercicio EscogerEjercicio()
    {
        Console.WriteLine("Que ejercicio deseas hacer ?");
        while (true)
        {
            var ejercicio_name = Console.ReadLine();
            if (C.Ejercicios.Any(x => x.Id == ejercicio_name))
            {
                return C.Ejercicios.Where(x => x.Id == ejercicio_name).First();
            }
            else
            {
                Console.WriteLine("Elige un ejercicio correcto");
            }
        }
    }
}

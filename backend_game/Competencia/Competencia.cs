using backend.Exercises;
using backend.Players;
namespace backend.Competence;

public class Competencia : IStopCondition
{
    public Competencia(params Ejercicio[] ejercicios)
    {
        Ejercicios = ejercicios;
    }
    public Ejercicio[] Ejercicios { get; }

    internal IDeable return_winner(Scorer scorer)
    {
        var max_points = scorer.puntos.Values.Select(x => x.Puntos).Max();
        var winner = scorer.puntos.Where(x => x.Value.Puntos == max_points);
        return winner.First().Key;
    }

    internal void ShowExcercises()
    {
        Console.WriteLine("La competencia posee los siguientes ejercicios:");
        foreach (var ejercicio in Ejercicios)
        {
            Console.WriteLine(ejercicio.ToString());
        }
    }
    public bool StopGameCondition(Player[] a)
    {
        return a.All(x => x.Musculos.Fuerza > 0);
    }
}

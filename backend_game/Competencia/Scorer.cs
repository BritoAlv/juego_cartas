using backend.Exercises;
using backend.Players;
namespace backend.Competence;
internal class Scorer
{
    public Dictionary<IDeable, Cosas_Hechas> puntos;
    public Scorer(IDeable[] jugadores, Competencia C)
    {
        this.puntos = new Dictionary<IDeable, Cosas_Hechas>();
        foreach (var player in jugadores)
        {
            puntos[player] = new Cosas_Hechas();
        }
    }
    public void Apply(IDeable player, IScoreExcercise ejercicio)
    {
        ejercicio.GetPoints(puntos[player]);
    }
}

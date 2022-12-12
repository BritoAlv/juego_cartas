using backend.Muscles; 
namespace backend.Exercises;
public abstract class Ejercicio : IConditionExcercise, IScoreExcercise, IChangesExcercise
{
    public Ejercicio(string id, Conditions conditions)
    {
        Id = id;
        Conditions = conditions;
    }
    public string Id { get; }
    public Conditions Conditions { get; }
    public abstract bool Puede_Hacer(IMusculos A, Conditions conditions); // depend on abstractions and not implementations.
    public abstract void GetPoints(Cosas_Hechas cosas_Hechas);
    public abstract void Cambios(IMusculos A);

    public override string ToString()
    {
        return Id + " " + Conditions.ToString();
    }
}

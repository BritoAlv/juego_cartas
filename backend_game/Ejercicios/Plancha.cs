using backend.Muscles;
namespace backend.Exercises;

public class Plancha : Ejercicio
{
    public Plancha(string id, Conditions conditions) : base(id, conditions)
    {
    }

    public override void Cambios(IMusculos A)
    {
        A.Musculos.Fuerza -= 10;
    }

    public override void GetPoints(Cosas_Hechas cosas_Hechas)
    {
        cosas_Hechas.Puntos = cosas_Hechas.Puntos + 10;
    }

    public override bool Puede_Hacer(IMusculos a, Conditions conditions)
    {
        return conditions.Required_Force <= a.Musculos.Fuerza;
    }
}
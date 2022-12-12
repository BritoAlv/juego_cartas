namespace backend.Exercises;
public class Conditions
{
    public Conditions(int required_force)
    {
        Required_Force = required_force;
    }
    public int Required_Force { get; }

    public override string ToString()
    {
        return "Requiere fuerza de :" + Required_Force.ToString();
    }
}

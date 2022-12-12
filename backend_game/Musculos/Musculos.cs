namespace backend.Muscles;
public class Musculos
{
    public Musculos(int fuerza)
    {
        Fuerza = fuerza;
    }

    public int Fuerza { get; set; }
    public override string ToString()
    {
        return Fuerza.ToString();
    }
}

public interface IMusculos
{
    Musculos Musculos { get; set; }
}

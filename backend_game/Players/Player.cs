using backend.Muscles;
namespace backend.Players;
public abstract class Player : IMusculos, IDeable
{
    public Player(string id, Musculos musculos)
    {
        Id = id;
        Musculos = musculos;
    }
    public string Id { get; }

    public Musculos Musculos { get ; set ; }

    public override string ToString()
    {
        string result = $"El jugador {Id} tiene unos músculos: {Musculos.ToString()}";
        return result;
    }
    public abstract void LogPlayerInfo();
}

internal interface IDeable
{
    public string Id{ get; }
}
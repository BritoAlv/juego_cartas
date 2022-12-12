using backend.Muscles;
namespace backend.Players;
public class NormalPlayer : Player
{
    public NormalPlayer(string id, Musculos musculos) : base(id, musculos)
    {

    }
    public override void LogPlayerInfo()
    {
        Console.WriteLine();
        Console.WriteLine(this.Id);
        Console.WriteLine(Musculos.ToString());
    }
}

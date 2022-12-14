using Poker;
namespace Game;

public class Computer_Player : Player
{
    public Computer_Player(string id, int dinero) : base(id, dinero)
    {
    }
    internal override int realizar_apuesta()
    {
        var apuesta = (int)Hand.rank >= 3 ? Dinero / 2 : 1;
        Console.WriteLine(apuesta);
        return apuesta;
    }
}

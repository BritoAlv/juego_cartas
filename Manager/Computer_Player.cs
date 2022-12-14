using Poker;
namespace Game;

public class Computer_Player : Player
{
    public Computer_Player(string id, int dinero) : base(id, dinero)
    {
    }
    internal override int realizar_apuesta()
    {
        if ((int)Hand.rank >= 3)
        {
            return Dinero / 2 ;
        }
        else
        {
            return 1 ;
        }
    }
}

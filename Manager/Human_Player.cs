namespace Game;

public class Human_Player : Player
{
    public Human_Player(string id, int dinero) : base(id, dinero)
    {
    }
    internal override int realizar_apuesta()
    {
        var a = (int)L.Use_Compiler();
        return a;
    }
}

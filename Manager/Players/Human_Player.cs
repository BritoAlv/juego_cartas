namespace Poker;
using LenguajeAPI;
public sealed class Human_Player : Player
{
    public Human_Player(string id, int dinero) : base(id, dinero)
    {
    }
    public override int realizar_apuesta(Bet Apuestas, IEnumerable<Player> Players, string info_apuesta)
    {
        var line = info_apuesta;
        if (string.IsNullOrEmpty(line))
        {
            return 0;
        }
        var a = Convert.ToInt32(API.Use_Compiler(line));
        return a;
    }
}

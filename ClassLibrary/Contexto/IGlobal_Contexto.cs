namespace Poker;

public interface IGlobal_Contexto
{
    IEnumerable<Player> Players { get; }
    Bet Apuestas { get; internal set; }
    List<Player> Active_Players { get; internal set;}
    List<Mini_Ronda_Contexto> Contextos { get; }
}

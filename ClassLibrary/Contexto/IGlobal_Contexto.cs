namespace Poker;

public interface IGlobal_Contexto
{
    IEnumerable<Player> Players { get; }
    List<Player> Active_Players { get; internal set;}
    IRonda_Context Ronda_Contexto {get; }
    void Config();
}

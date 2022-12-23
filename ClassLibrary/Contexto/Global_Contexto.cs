namespace Poker;
/// <summary>
/// This class represent the Context that a player may use to determine its decision. From the Player point the Context
/// is an immutable object.
/// </summary>
public class Global_Contexto : IGlobal_Contexto
{
    public Global_Contexto(IRonda_Context ronda_Context, params Player[] players)
    {
        Players = players;
        Ronda_Contexto = ronda_Context;
        Active_Players = Players.ToList();
    }
    public IEnumerable<Player> Players { get; }
    private Ronda_Context Ronda_Context { get; }
    public List<Player> Active_Players { get; set; }
    public IRonda_Context Ronda_Contexto{ get; }
    public void Config()
    {
        Ronda_Contexto.Apuestas = new Bet(this.Active_Players);
        Ronda_Contexto.CardsManager = new CardManager(this.Active_Players);
    }
}
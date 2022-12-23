namespace Poker;
/// <summary>
/// This class represent the Context that a player may use to determine its decision. From the Player point the Context
/// is an immutable object.
/// </summary>
public class Global_Contexto : IGlobal_Contexto
{
    public Global_Contexto(IRonda_Context ronda_Context, params Player[] players)
    {
        Ronda_Contexto = ronda_Context;
        PlayerManager = new PlayerManager(players);
    }
    public PlayerManager PlayerManager { get; }
    public IRonda_Context Ronda_Contexto { get; }
    public void Config()
    {
        Ronda_Contexto.Apuestas = new Bet(this.PlayerManager.Get_Active_Players(1));
        Ronda_Contexto.CardsManager = new CardManager(this.PlayerManager.Get_Active_Players(1));
        Ronda_Contexto.Participants = this.PlayerManager.Get_Active_Players(1);
    }
}
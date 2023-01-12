namespace Poker;
public interface IGlobal_Contexto
{
    PlayerManager PlayerManager { get; }
    IRonda_Context Ronda_Contexto { get; }
    Dictionary<string, object> variables { get; set; }
    Factory factory { get; }
    void Config();
    List<Return<bool>> efectos {get;set;}
}

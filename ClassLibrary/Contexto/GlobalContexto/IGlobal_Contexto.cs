namespace Poker;
public interface IGlobal_Contexto
{
    PlayerManager PlayerManager { get; }
    IRonda_Context Ronda_Contexto { get; }
    Dictionary<string, object> variables { get; set; }
    Factory factory { get; }
    List<FinalRoundEffect> FinalRoundEffects { get; set; }
    void Config();
}

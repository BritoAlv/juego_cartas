namespace Poker;
public interface IRonda_Context
{
    Bet Apuestas { get; internal set;}
    CardManager CardsManager { get; internal set;}
    List<Mini_Ronda_Contexto> Contextos { get;}
}

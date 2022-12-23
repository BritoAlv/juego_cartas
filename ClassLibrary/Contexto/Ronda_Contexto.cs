namespace Poker;
public class Ronda_Context
{
    private Bet? _apuestas;
    private CardManager? _CardsManager;
    public Ronda_Context(List<Mini_Ronda_Contexto> contextos)
    {
        Contextos = contextos;
    }
    public Bet Apuestas
    {
        get
        {
            if (_apuestas is null)
            {
                throw new Exception("IDK");
            }
            return _apuestas;
        }
        internal set
        {
            _apuestas = value;
        }
    }
    public CardManager CardsManager
    {
        get
        {
            if (_CardsManager is null)
            {
                throw new Exception("IDK");
            }
            return _CardsManager;
        }
        internal set
        {
            _CardsManager = value;
        }
    }
    public List<Mini_Ronda_Contexto> Contextos { get; }
}
namespace Poker;
public class Ronda_Context : IRonda_Context
{
    private Bet? _apuestas;
    private CardManager? _CardsManager;
    public Ronda_Context(List<Mini_Ronda_Contexto> contextos)
    {
        Contextos = contextos;
        final_efectos = new List<Return<bool>>();
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
        set
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
        set
        {
            _CardsManager = value;
        }
    }
    public List<Mini_Ronda_Contexto> Contextos { get; }
    public List<Return<bool>> final_efectos{ get; set; }
}
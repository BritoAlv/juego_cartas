using System.Diagnostics.CodeAnalysis;
namespace Poker;

/// <summary>
/// Defines all the requirements that a player should obey.
/// </summary>
public abstract partial class Player : Ideable, IApostador, IDescribable<Player>, IEqualityComparer<Player>
{
    /// <summary>
    /// This have the responsibility of deal with effects.
    /// </summary>
    public IColector Colector { get; private set; }
    protected Player(string id, int dinero)
    {
        Id = id;
        Dinero = dinero;
        Colector = new Colector(id);
    }
    public string Id { get; }
    public int Dinero { get; set; }
    private Hand? _hand;
    public Hand Hand
    {
        get
        {
            if (_hand is null)
            {
                throw new Exception("IDK");
            }
            else
            {
                return _hand;
            }
        }
        set
        {
            _hand = value;
        }
    }
    /*
    THis defines the logic of what decision take.
    */
    public abstract IDecision parse_decision(IGlobal_Contexto contexto);
    /*
    This defines the logic of how much money the player should bet.
    */
    public abstract int realizar_apuesta(IGlobal_Contexto contexto);
    
    public bool Equals(Player? x, Player? y)
    {
        if (x is null || y is null)
        {
            return false;
        }
        if (x.Id == y.Id)
        {
            return true;
        }
        return false;
    }
    public int GetHashCode([DisallowNull] Player obj)
    {
        return obj.Id.GetHashCode();
    }
    public List<int> Apuestas { get; set; } = new List<int>();
}
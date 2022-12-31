using System.Diagnostics.CodeAnalysis;

namespace Poker;
public abstract partial class Player : Ideable, IApostador, IDescribable<Player>, IEqualityComparer<Player>, IColector
{
    public JsonColector Colector { get; private set; }
    public List<string> get_efectos => Colector.get_efectos;
    protected Player(string id, int dinero)
    {
        Id = id;
        Dinero = dinero;
        Colector = new JsonColector(id);
    }
    public string Id { get; }
    internal int Dinero { get; set; }
    private Hand? _hand;
    internal Hand Hand
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
    public abstract IDecision parse_decision(IGlobal_Contexto contexto);
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
    public void add_efecto(string efecto)
    {
        Colector.add_efecto(efecto);
    }

    public string remove_efecto()
    {
        return Colector.remove_efecto();
    }
    public List<int> Apuestas { get; set; } = new List<int>();
}
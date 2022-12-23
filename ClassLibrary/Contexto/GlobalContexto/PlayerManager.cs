namespace Poker;
/// <summary>
/// This class will deal with the players, for example the ActivePlayers are the ones 
/// </summary>
public class PlayerManager
{
    internal PlayerManager(Player[] players)
    {
        Players = players;
        Active_Players = Players.ToList();
        _filtro_ronda = null;
    }
    /// <summary>
    /// This are the players that started the game.
    /// </summary>
    public IEnumerable<Player> Players { get; }
    /// <summary>
    /// This represents the Active Players, modifying this, will blow up this thing. 
    /// </summary>
    private List<Player> Active_Players { get; set; }
    public delegate bool Filtrar(Player player);
    private Filtrar? _filtro_partida;
    public Filtrar Filtro_Partida
    {
        get
        {
            if (_filtro_partida is null)
            {
                return x => true;
            }
            return _filtro_partida;
        }
        set
        {
            _filtro_partida = value;
        }
    }
    private Filtrar? _filtro_ronda;
    public Filtrar Filtro_Ronda
    {
        get
        {
            if (_filtro_ronda is null)
            {
                return x => true;
            }
            return _filtro_ronda;
        }
        set
        {
            _filtro_ronda = value;
        }
    }
    private Filtrar? _filtro_mini_ronda;
    public Filtrar Filtro_Mini_Ronda
    {
        get
        {
            if (_filtro_mini_ronda is null)
            {
                return x => true;
            }
            return _filtro_mini_ronda;
        }
        set
        {
            _filtro_mini_ronda = value;
        }
    }
    public List<Player> Get_Active_Players(int level)
    {
        if (level == 1)
        {
            return this.Active_Players.Where(x => Filtro_Partida(x)).ToList();
        }
        if (level == 2)
        {
            return Get_Active_Players(1).Where(x => Filtro_Ronda(x)).ToList();
        }
        if (level == 3)
        {
            return Get_Active_Players(2).Where(x => Filtro_Mini_Ronda(x)).ToList();
        }
        throw new Exception("IDK");
    }
    internal void Set_Active_Players(List<Player> players)
    {
        this.Active_Players = players;
    }
    internal Player Get_Player_By_Pos(int i)
    {
        return this.Active_Players[i];
    }
}
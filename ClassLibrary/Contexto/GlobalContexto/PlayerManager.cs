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
        _filtro_ronda = new List<Filtrar>();
        _filtro_mini_ronda = new List<Filtrar>();
        _filtro_partida = new List<Filtrar>();
    }
    /// <summary>
    /// This are the players that started the game.
    /// </summary>
    public IEnumerable<Player> Players { get; }

    /// <summary>
    /// This represents the Active Players, modifying this, will blow up this thing. 
    /// </summary>
    private List<Player> Active_Players { get; set; }

    /// <summary>
    /// I'm introducing the concept of Filter to start adding weird things to the Poker.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public delegate bool Filtrar(Player player);
    private List<Filtrar> _filtro_partida;
    public List<Filtrar> Filtro_Partida
    {
        get
        {
            if (_filtro_partida is null)
            {
                _filtro_partida = new List<Filtrar>();
            }
            return _filtro_partida;
        }
        set
        {
            _filtro_partida = value;
        }
    }
    private List<Filtrar> _filtro_ronda;
    public List<Filtrar> Filtro_Ronda
    {
        get
        {
            if (_filtro_ronda is null)
            {
                _filtro_ronda = new List<Filtrar>();
            }
            return _filtro_ronda;
        }
        set
        {
            _filtro_ronda = value;
        }
    }
    private List<Filtrar> _filtro_mini_ronda;
    public List<Filtrar> Filtro_Mini_Ronda
    {
        get
        {
            if (_filtro_mini_ronda is null)
            {
                _filtro_mini_ronda = new List<Filtrar>();
            }
            return _filtro_mini_ronda;
        }
        set
        {
            _filtro_mini_ronda = value;
        }
    }
    public Player? Current { get; set; }
    public IEnumerable<Player> Get_Active_Players(int level)
    {
        if (level == 1)
        {
            foreach (var player in Active_Players)
            {
                if (Eval_Filtro_Partida(player))
                {
                    yield return player;
                }
            }
            yield break;
        }
        if (level == 2)
        {
            foreach (var player in Active_Players)
            {
                if (Eval_Filtro_Partida(player) && Eval_Filtro_Ronda(player))
                {
                    yield return player;
                }
            }
            yield break;
        }
        if (level == 3)
        {
            foreach (var player in Active_Players)
            {
                if (Eval_Filtro_Partida(player) && Eval_Filtro_Ronda(player) && Eval_Filtro_Mini_Ronda(player))
                {
                    yield return player;
                }
            }
            yield break;
        }
        throw new Exception("IDK");
    }
    private bool Eval_Filtro_Mini_Ronda(Player player)
    {
        foreach (var filter in this.Filtro_Mini_Ronda)
        {
            if (!filter(player))
            {
                return false;
            }
        }
        return true;
    }
    private bool Eval_Filtro_Ronda(Player player)
    {
        foreach (var filter in this.Filtro_Ronda)
        {
            if (!filter(player))
            {
                return false;
            }
        }
        return true;
    }
    private bool Eval_Filtro_Partida(Player player)
    {
        foreach (var filter in this.Filtro_Partida)
        {
            if (!filter(player))
            {
                return false;
            }
        }
        return true;
    }
    internal void Set_Active_Players(List<Player> players)
    {
        this.Active_Players = players;
    }
    internal Player Get_Player_By_Pos(int i)
    {
        return this.Active_Players[i];
    }
    internal void Shufle_Players()
    {
        var player = this.Active_Players[0];
        this.Active_Players.RemoveAt(0);
        this.Active_Players.Add(player);
    }
}
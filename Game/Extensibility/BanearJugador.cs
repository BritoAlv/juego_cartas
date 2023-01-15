namespace Poker;
public class BanearJugador : Return<bool>
{
    public BanearJugador(Token open_parenthesis, Token signature, IArgument<Player> player, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Player = player;
    }
    public IArgument<Player> Player { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        var players = Player.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto);
        foreach (var player in players)
        {
            contexto.PlayerManager.Filtro_Mini_Ronda.Add(x => x.Id != player.Id);
        }
        return new List<bool> { true };
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).First();
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        if (Player is not null)
        {
            yield return Player;
        }
        yield return Closed_Parenthesis;
    }
}

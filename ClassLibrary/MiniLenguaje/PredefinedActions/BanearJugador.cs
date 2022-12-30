namespace Poker;
public class BanearJugador : Return<bool>
{
    public BanearJugador(Token open_parenthesis, Token signature, IArgument<Player> player, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Player = player;
    }

    public IArgument<Player> Player { get; }

    public override bool Evaluate(IGlobal_Contexto contexto)
    {
        var player = Player.Get_Object(contexto.PlayerManager.Get_Active_Players(2), contexto);
        contexto.PlayerManager.Filtro_Mini_Ronda.Add(x => x.Id != player.Id);
        return true;
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

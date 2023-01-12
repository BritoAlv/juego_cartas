namespace Poker;
public class Getdinero : Return<int>
{
    public Getdinero(Token open_parenthesis, Token signature, IArgument<Player> player,  Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        source_Player = player;
    }
    public IArgument<Player> source_Player { get; }
    public override IEnumerable<int> Evaluate(IGlobal_Contexto contexto)
    {
        IEnumerable<Player> players = source_Player.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto);
        foreach (var player in players)
        {
            int obtained = player.Dinero;
            return new List<int> { obtained};
        }
        return new List<int>();
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).Count() > 0;
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return source_Player;
        yield return Closed_Parenthesis;
    }
}
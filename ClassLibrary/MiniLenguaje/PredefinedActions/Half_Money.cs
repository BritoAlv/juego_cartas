namespace Poker;
public class Half_Money : Return<bool>
{
    public Half_Money(Token open_parenthesis, Token signature, IArgument<Player> player,  Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        source_Player = player;
    }
    public IArgument<Player> source_Player { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        Player player = source_Player.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto).First();
        if (!contexto.variables.ContainsKey("dinero" + $"{player.Id}"))
        {
            contexto.variables["dinero" + $"{player.Id}"] = player.Dinero;
            contexto.efectos.Add(this);
        }
        else
        {
            player.Dinero += Math.Abs((player.Dinero - (int)contexto.variables["dinero" + $"{player.Id}"])) / 2;
        }
        return new List<bool>();
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
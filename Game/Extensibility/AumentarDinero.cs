using Poker;

public class AumentarDinero : Return<int>
{
    public AumentarDinero(Token open_parenthesis, Token signature, Token argumento, IArgument<Player> player, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Argumento = argumento;
        Player = player;
    }
    public Token Argumento { get; }
    public IArgument<Player> Player { get; }
    public override IEnumerable<int> Evaluate(IGlobal_Contexto contexto)
    {
        int dinero = (int)Eval.Evaluador.Evaluator(Operacion.Replaace(Argumento.Text, contexto));
        foreach (var player in Player.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto))
        {
            player.Dinero += dinero;
        }
        return new List<int> { dinero };
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        var dinero = Evaluate(contexto).FirstOrDefault();
        return true;
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        yield return Argumento;
        yield return Player;
        yield return Closed_Parenthesis;
    }
}
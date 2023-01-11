namespace Poker;
using Eval;
public class ModificarValorCarta : Return<bool>
{
    public ModificarValorCarta(Token open_parenthesis, Token signature, Token argumento, IArgument<Card> card, IArgument<Player> player , Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        Argumento = argumento;
        source_Card = card;
        source_Player = player;
    }
    public Token Argumento { get; }
    public IArgument<Card> source_Card { get; }
    public IArgument<Player> source_Player { get; }

    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        string operation = Operacion.Replaace(Argumento.Text, contexto);
        IEnumerable<Player> players = source_Player.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto);
        foreach (var player in players)
        {
            Card? obtained = source_Card.Get_Objects(player.Hand.Cards, contexto).FirstOrDefault();
            if (obtained is null)
            {
                continue;
            }
            else
            {
                obtained.change_value((int)(Eval.Evaluador.Evaluator(operation)));
                return new List<bool> { true };
            }
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
        yield return Argumento;
        yield return source_Card;
        yield return source_Player;
        yield return Closed_Parenthesis;
    }
}
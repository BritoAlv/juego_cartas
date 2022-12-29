namespace Poker;
public partial class Evaluator
{
    Card? Current_Card = null;
    Player? Current_Player = null;
    public Evaluator(CompoundAction action, IGlobal_Contexto contexto)
    {
        Action = action;
        Contexto = contexto;
        Generator = new Func_Generator(contexto);
    }
    public CompoundAction Action { get; }
    public IGlobal_Contexto Contexto { get; }
    public Func_Generator Generator { get; }
    public int Evaluate()
    {
        return ExecuteAction(Action);
    }
    private int ExecuteAction(CompoundAction action)
    {
        if (action is IArgument<Card>)
        {
            Current_Card = EvaluateFind_Card((ActionCard)action);
            if (Current_Card is null)
            {
                return 0;
            }
            return 1;
        }
        if (action is IArgument<Player>)
        {
            Current_Player = EvaluateFind_PLayer((ActionPlayer)action);
            if (Current_Player is null)
            {
                return 0;
            }
            return 1;
        }
        else
        {
            return EvaluateVoidAction(action);
        }
    }
    private int EvaluateVoidAction(CompoundAction action)
    {
        switch (action.Signature.Text)
        {
            case "$void_añadircarta":
                return EvaluateAñadirCarta(action.Find_Player, action.Find_Card);
            default:
                return 0;
        }
    }
    private Player? EvaluateFind_PLayer(ActionPlayer action)
    {
        switch (action.Signature.Text)
        {
            default:
                return null;
        }
    }
    private Card? EvaluateFind_Card(ActionCard action)
    {
        switch (action.Signature.Text)
        {
            case "$carta_robar":
                return EvaluateRobarCarta(action.Find_Player, action.Find_Card);
            default:
                return null;
        }
    }

}
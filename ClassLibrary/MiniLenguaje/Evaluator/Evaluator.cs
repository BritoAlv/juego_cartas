namespace Poker;

public class Evaluator
{
    public Evaluator(CompoundAction action, IGlobal_Contexto contexto)
    {
        Action = action;
        Contexto = contexto;
    }

    public CompoundAction Action { get; }
    public IGlobal_Contexto Contexto { get; }

    public int Evaluate()
    {
        switch (Action.Signature.Text)
        {
            case "$añadircarta":
                return EvaluarAñadirCarta(Action);
            default:
                return 0;
        }
    }

    private int EvaluarAñadirCarta(CompoundAction action)
    {

        if (action.Find_Card is null || action.Find_Player is null)
        {
            return 0;
        }
        var comp = (ActionCard)action.Find_Card;
        if (action.Find_Card.get_card.Count == 0)
        {
            switch (comp.Signature.Text)
            {
                case "$robarcarta":
                    action.Find_Card.get_card.Add(CreateRobarCartaFunc(comp));
                    break;
                default:
                    break;
            }
        }

        Player? obtained_player = GetPlayer(action.Find_Player);
        if (obtained_player is null)
        {
            return 0;
        }
        Card? obtained_card = GetCard(action.Find_Card);
        if (obtained_card is null)
        {
            return 0;
        }
        Contexto.Ronda_Contexto.CardsManager.AñadirCarta(obtained_player, obtained_card);
        return 1;
    }

    private Card? GetCard(IFindCard? action)
    {
        if (action is null)
        {
            return null;
        }
        if (action is CompoundAction compound)
        {
            Player? obtained_player = GetPlayer(compound.Find_Player);
            if (obtained_player is null)
            {
                return null;
            }
            Card? obtained_card = null;
            foreach (var Func in action.get_card)
            {
                obtained_card = Func(Contexto.Ronda_Contexto.CardsManager.Cards[obtained_player]);
                if (obtained_card is not null)
                {
                    Contexto.Ronda_Contexto.CardsManager.RemoverCarta(obtained_player, obtained_card);
                    return obtained_card;
                }
            }
        }
        return null;

    }

    private Player? GetPlayer(IFindPlayer? action)
    {
        if (action is null)
        {
            return null;
        }
        Player? obtained_player = null;
        foreach (var Func in action.get_player)
        {
            obtained_player = Func(Contexto.PlayerManager.Get_Active_Players(2));
            if (obtained_player is not null)
            {
                break;
            }
        }

        return obtained_player;
    }

    private Func<IEnumerable<Card>, Card?> CreateRobarCartaFunc(ActionCard comp)
    {
        var player = GetPlayer(comp.Find_Player);
        if (player is null)
        {
            return x => null;
        }
        Card? obtained_card = null;
        foreach (var Func in comp.Find_Card!.get_card)
        {
            obtained_card = Func(Contexto.Ronda_Contexto.CardsManager.Cards[player]);
            if (obtained_card is not null)
            {
                return Func;
            }
        }
        return x => null;
    }
}
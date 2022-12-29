namespace Poker;
public partial class Evaluator
{
    private int EvaluateAñadirCarta(IFindPlayer? find_Player, IFindCard? find_Card)
    {
        Player? player = GetPlayer(find_Player);
        if (player is null)
        {
            return 0;
        }
        Card? card = GetCard(find_Card, player);
        if (card is null)
        {
            return 0;
        }
        Contexto.Ronda_Contexto.CardsManager.AñadirCarta(player, card);
        return 1;
    }
    private Card? EvaluateRobarCarta(IFindPlayer? find_Player, IFindCard? find_Card)
    {
        Player? player = GetPlayer(find_Player);
        if (player is null)
        {
            return null;
        }
        Card? card = GetCard(find_Card, player);
        if (card is null)
        {
            return null;
        }
        Contexto.Ronda_Contexto.CardsManager.RemoverCarta(player, card);
        return card;
    }
    private Card? GetCard(IFindCard? find_Card, Player player)
    {
        if (find_Card is null)
        {
            return null;
        }
        else if (find_Card is LiteralDescribeCard describeCard)
        {
            Card? obtained_card = null;
            foreach (var Func in Generator.GetCardFunction(describeCard.Arguments))
            {
                obtained_card = Func(Contexto.Ronda_Contexto.CardsManager.Cards[player]).FirstOrDefault(x => x != null);
                if (obtained_card is not null)
                {
                    Current_Card = obtained_card;
                    return Current_Card;
                }
            }
            return obtained_card;
        }
        else if (find_Card is CompoundAction action)
        {
            ExecuteAction(action);
            return Current_Card;
        }
        return null;
    }
    private Player? GetPlayer(IFindPlayer? find_Player)
    {
        if (find_Player is null)
        {
            return null;
        }
        else if (find_Player is LiteralDescribePlayer describePlayer)
        {
            Player? obtained_player = null;
            foreach (var Func in Generator.GetPlayerFunction(describePlayer.Arguments))
            {
                obtained_player = Func(Contexto.PlayerManager.Get_Active_Players(2)).FirstOrDefault(x => x != null);
                if (obtained_player is not null)
                {
                    Current_Player = obtained_player;
                    return Current_Player;
                }
            }
            return obtained_player;
        }
        else if (find_Player is CompoundAction action)
        {
            ExecuteAction(action);
            return Current_Player;
        }
        return null;
    }
}
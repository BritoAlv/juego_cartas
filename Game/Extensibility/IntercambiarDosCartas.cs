/*
This file contains customs classes to demonstrate extensibility.
*/
using Poker;
/*
exchange two random cards of two players. new predefined action that takes two player as argument.
*/
public class IntercambiarDosCartas : Return<bool>
{
    public IntercambiarDosCartas(Token open_parenthesis, Token signature, IArgument<Player> first, IArgument<Player> second, Token closed_parenthesis) : base(open_parenthesis, signature, closed_parenthesis)
    {
        First = first;
        Second = second;
    }
    public IArgument<Player> First { get; }
    public IArgument<Player> Second { get; }
    public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
    {
        var player1 = First.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto).First();
        var player2 = Second.Get_Objects(contexto.PlayerManager.Get_Active_Players(2), contexto).First();
        if (contexto.Ronda_Contexto.CardsManager.Get_PLayer_Cards(player1).Length < 2 && contexto.Ronda_Contexto.CardsManager.Get_PLayer_Cards(player2).Length < 2 )
        {
            return new List<bool> { false };
        }
        var card1 = contexto.Ronda_Contexto.CardsManager.Remove_Random_Card(player1);
        var card2 = contexto.Ronda_Contexto.CardsManager.Remove_Random_Card(player1);
        var card3 = contexto.Ronda_Contexto.CardsManager.Remove_Random_Card(player2);
        var card4 = contexto.Ronda_Contexto.CardsManager.Remove_Random_Card(player2);
        contexto.Ronda_Contexto.CardsManager.AñadirCarta(player1, card3);
        contexto.Ronda_Contexto.CardsManager.AñadirCarta(player1, card4);
        contexto.Ronda_Contexto.CardsManager.AñadirCarta(player2, card1);
        contexto.Ronda_Contexto.CardsManager.AñadirCarta(player2, card2);
        return new List<bool> { true };
    }
    public override bool Evaluate_Top(IGlobal_Contexto contexto)
    {
        return Evaluate(contexto).First();
    }
    public override IEnumerable<Iprintable> GetChildrenIprintables()
    {
        yield return Open_Parenthesis;
        if (First is not null)
        {
            yield return First;
        }
        if (Second is not null)
        {
            yield return Second;
        }
        yield return Closed_Parenthesis;
    }
}
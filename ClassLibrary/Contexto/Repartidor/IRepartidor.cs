namespace Poker;

/// <summary>
/// 
/// </summary>
public interface IRepartidor
{
    List<Card> RepartirCartas(Player player, int max_Cant_Cartas, in Dictionary<Ideable, List<Card>> cartas_asignadas, Func<Player, Card> card_Generator);
}
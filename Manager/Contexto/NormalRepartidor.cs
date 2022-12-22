namespace Poker;

/// <summary>
/// Does everything as expected.
/// </summary>
internal class NormalRepartidor : IRepartidor
{
    public List<Card> RepartirCartas(Player player, int max_Cant_Cartas, in Dictionary<Ideable, List<Card>> cartas_asignadas, Func<Player, Card> card_Generator)
    {
        List<Card> result = new List<Card>();
        for (int i = 0; i < max_Cant_Cartas; i++)
        {
            result.Add(card_Generator(player));
        }
        return result;
    }
}

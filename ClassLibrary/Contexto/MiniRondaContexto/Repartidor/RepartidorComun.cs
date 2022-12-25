namespace Poker;

public sealed class RepartidorComun : Repartidor
{
    List<Card>? common_cards = null;
    public override List<Card> Repartir_Cartas(Player player, int cant_Cartas, IEnumerable<KeyValuePair<Ideable, List<Card>>> cards)
    {
        if (common_cards is null) 
        {
            common_cards = new List<Card>();
            for (int i = 0; i < cant_Cartas; i++)
            {
                common_cards.Add(Random_Utils.generate_random_card());
            }
        }
        return common_cards;
    }
}
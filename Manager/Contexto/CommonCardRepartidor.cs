namespace Poker;
public class CommonCardRepartidor : IRepartidor
{
    private List<Card>? _cards;
    public List<Card> RepartirCartas(Player player, int max_Cant_Cartas, in Dictionary<Ideable, List<Card>> cartas_asignadas, Func<Player, Card> card_Generator)
    {
        if (this._cards == null)
        {
            _cards = new List<Card>();
            for (int i = 0; i < max_Cant_Cartas; i++)
            {
                var card = random.generate_random_card();
                this._cards.Add(card);
            }
        }
        return _cards.AsEnumerable().Select(x => (Card)x.Clone()).ToList();
    }
}
namespace Poker;
public class CardManager
{
    internal Dictionary<Ideable, List<Card>> Cards { get; private set; }
    internal CardManager(IEnumerable<Player> Players)
    {
        Cards = new Dictionary<Ideable, List<Card>>();
        foreach (var player in Players)
        {
            Cards[player] = new List<Card>();
        }
    }
    internal void AñadirCarta(Player A, Card card)
    {
        Cards[A].Add(card);
        A.Hand.Draw(card);
    }
    internal void RepartirCartas(Player player, int cant_Cartas)
    {
        for (int i = 0; i < cant_Cartas; i++)
        {
            AñadirCarta(player, random.generate_random_card());
        }
    }
}
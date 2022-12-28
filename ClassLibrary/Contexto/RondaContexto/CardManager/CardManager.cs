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

    internal void RemoverCarta(Player A, Card card)
    {
        Cards[A].Remove(card);
        A.Hand.Remove(x => x.Value == card.Value && x.Suit == card.Suit);
    }

    internal void AñadirCartas(IRepartidor repartidor, Player A, int cant_Cartas)
    {
        List<Card> cards = repartidor.Repartir_Cartas(A, cant_Cartas, Cards.AsEnumerable());
        if (cant_Cartas > cards.Count)
        {
            throw new Exception("Me la estas jugando");
        }
        else
        {
            foreach (var card in cards)
            {
                AñadirCarta(A, card);
            }
        }
    }
}
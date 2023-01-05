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
    public void AñadirCarta(Player A, Card card)
    {
        Cards[A].Add(card);
        A.Hand.Draw(card);
    }

    public void RemoverCarta(Player A, Card card)
    {
        Cards[A].Remove(card);
        A.Hand.Remove(x => x.Value == card.Value && x.Suit == card.Suit);
    }

    public Card Remove_Random_Card(Player A)
    {
        Random random = new Random();
        var position = random.Next(A.Hand.Cards.Count());
        var card = A.Hand.Cards.ElementAt(position);
        RemoverCarta(A, card);
        return card;

    }

    public void AñadirCartas(IRepartidor repartidor, Player A, int cant_Cartas)
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

    public Card[] Get_PLayer_Cards(Player A)
    {
        return A.Hand.Cards.ToArray();
    }

    public IEnumerable<Card> All_Cards()
    {
        foreach (var list_card in Cards.Values)
        {
            foreach (var card in list_card)
            {
                yield return card;
            }
        }
    }
}
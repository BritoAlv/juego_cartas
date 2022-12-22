namespace Poker;
public class Mini_Ronda_Contexto
{
    private Func<Player, Card>? _card_generator;
    public Mini_Ronda_Contexto(int cant_cartas, Func<Player, Card>? card_Generator)
    {
        _card_generator = card_Generator;
        Cant_Cartas = cant_cartas;
    }
    public Mini_Ronda_Contexto(int cant_cartas) : this(cant_cartas, null) { }
    public Func<Player, Card> Card_Generator
    {
        get
        {
            if (_card_generator is null)
            {
                return generate_random_card;
            }
            return _card_generator;
        }
        internal set
        {
            _card_generator = value;
        }
    }
    public int Cant_Cartas { get; }
    public static Card generate_random_card(Player A)
    {
        return random.generate_random_card();
    } 
    internal Dictionary<Ideable, List<Card>> AssignCards(IEnumerable<Player> Participants)
    {
        Dictionary<Ideable, List<Card>> result = new Dictionary<Ideable, List<Card>>();
        foreach (var participant in Participants)
        {
            List<Card> assigned = new List<Card>();
            for (int i = 0; i < this.Cant_Cartas; i++)
            {
                assigned.Add(this.Card_Generator(participant));
            }
            result[participant] = assigned;
        }
        return result;
    }
}
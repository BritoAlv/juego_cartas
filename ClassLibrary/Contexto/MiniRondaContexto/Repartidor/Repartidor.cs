namespace Poker;

public class Repartidor : IRepartidor
{
    private Func<Player, Card>? _card_generator;
    public Repartidor(): this(null)
    {

    }
    public Repartidor(Func<Player, Card>? card_generator)
    {
        _card_generator = card_generator;
    }
    public virtual List<Card> Repartir_Cartas(Player player, int cant_Cartas, IEnumerable<KeyValuePair<Ideable, List<Card>>> cards)
    {
        List<Card> result = new List<Card>();
        for (int i = 0; i < cant_Cartas; i++)
        {
            result.Add(random_generator(player));
        }
        return result;
    }
    public Func<Player, Card> Card_generator
    {
        get
        {
            if (_card_generator is null)
            {
                return random_generator;
            }
            return _card_generator;
        }
        set
        {
            _card_generator = value;
        }
    }
    private static Card random_generator(Player A)
    {
        return Random_Utils.generate_random_card();
    }
}

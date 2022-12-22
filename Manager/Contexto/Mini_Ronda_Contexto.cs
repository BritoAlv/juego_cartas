namespace Poker;
public class Mini_Ronda_Contexto
{
    private Func<Player ,Card>? _card_generator;
    public Mini_Ronda_Contexto(int cant_cartas, Func<Player ,Card>? card_Generator)
    {
        _card_generator = card_Generator;
        Cant_Cartas = cant_cartas;
    }
    public Mini_Ronda_Contexto(int cant_cartas) : this(cant_cartas, null){}
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
}
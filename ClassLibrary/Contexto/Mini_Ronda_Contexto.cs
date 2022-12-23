namespace Poker;
public class Mini_Ronda_Contexto
{
    private Func<Player, Card>? _card_generator;
    private IRepartidor? _repartidor;
    public Mini_Ronda_Contexto(int cant_cartas, IRepartidor? repartidor, Func<Player, Card>? card_Generator)
    {
        _card_generator = card_Generator;
        Cant_Cartas = cant_cartas;
        _repartidor = repartidor;
    }
    public Mini_Ronda_Contexto(int cant_cartas, IRepartidor repartidor) : this(cant_cartas, repartidor, null) { }
    public Mini_Ronda_Contexto(int cant_cartas) : this(cant_cartas, null, null) { }
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
    public IRepartidor Repartidor
    {
        get
        {
            if (_repartidor is null)
            {
                return new NormalRepartidor();
            }
            return _repartidor;
        }
    }
    public int Cant_Cartas { get; }
    public static Card generate_random_card(Player A)
    {
        return random.generate_random_card();
    }
    internal List<Card> RepartirCartas(Player player, in Dictionary<Ideable, List<Card>> cartas_asignadas) => Repartidor.RepartirCartas(player, Cant_Cartas, cartas_asignadas, Card_Generator);
}

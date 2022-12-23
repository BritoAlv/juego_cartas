namespace Poker;

public interface IRepartidor
{
    List<Card> Repartir_Cartas(Player player, int cant_Cartas, IEnumerable<KeyValuePair<Ideable, List<Card>>> cards);
    Func<Player, Card> Card_generator {get; internal set;}
}

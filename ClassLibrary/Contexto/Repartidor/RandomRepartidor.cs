namespace Poker;

internal class RandomRepartidor : IRepartidor
{
    private static Card generate_random_card(Player A)
    {
        return random.generate_random_card();
    }

    public Card RepartirCarta(Player player)
    {
        return generate_random_card(player);
    }
}
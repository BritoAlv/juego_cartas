using NeoSmart.Unicode;
/*
Enums have default values.
*/
namespace Poker;
public enum CardSuit
{
    CorazónRojo,
    Pica,
    Trébol,
    Diamante,
}

internal static class Extensor
{
    public static NeoSmart.Unicode.SingleEmoji GetEmoji(this CardSuit suit)
    {
        switch (suit)
        {
            case CardSuit.CorazónRojo:
                return Emoji.RedHeart;
            case CardSuit.Diamante:
                return Emoji.DiamondSuit;
            case CardSuit.Pica:
                return Emoji.SpadeSuit;
            default:
                return Emoji.ClubSuit;
        }
    }
}
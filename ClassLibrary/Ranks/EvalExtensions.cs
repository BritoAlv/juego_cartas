namespace Poker;

internal static class EvalExtensions
{
    public static bool HasOfAKind(IEnumerable<Card> cards, int num) => cards.ToKindAndQuantities().Any(c => c.Value >= num);
    public static int CountOfAKind(IEnumerable<Card> cards, int num) => cards.ToKindAndQuantities().Count(c => c.Value == num);
    public static IEnumerable<KeyValuePair<CardValue, int>> ToKindAndQuantities(this IEnumerable<Card> cards)
    {
        var dict = new Dictionary<CardValue, int>();
        foreach (var card in cards)
        {
            if (!dict.ContainsKey(card.Value))
            {
                dict[card.Value] = 0;
            }
            dict[card.Value]++;
        }
        return dict;
    }

    // The SelectConsecutive method iterates over two consecutive items in a collection.
    // This is done using the yield keyword
    // Each call to the iterator function proceeds to the next execution of the yield return statement
    // This method is very similar to the source code found in LINQ methods.
    public static IEnumerable<TResult> SelectConsecutive<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TSource, TResult> selector)
    {
        int index = -1;
        foreach (TSource element in source.Take(source.Count() - 1)) // skip the last, it will be evaluated by source.ElementAt(index + 1)
        {
            checked { index++; } // explicitly enable overflow checking
            yield return selector(element, source.ElementAt(index + 1)); // delegate element and element[+1] to Func<TSource, TSource, TResult>
        }
    }
}
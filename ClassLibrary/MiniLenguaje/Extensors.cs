namespace Poker;
/*
some extensors methods to perform effects.
*/
public static class Extensors
{
    public static int Summ(this IEnumerable<int> to_sum)
    {
        if (to_sum.Count() == 0)
        {
            return 0;
        }
        return to_sum.Sum();
    }

    public static int Maxx(this IEnumerable<int> to_max)
    {
        if (to_max.Count() == 0)
        {
            return 0;
        }
        return to_max.Max();
    }

    public static IEnumerable<T> Intersectt<T>(this IEnumerable<T> first, IEnumerable<T> other) where T : IEqualityComparer<T>
    {
        List<T> result = new List<T>();
        foreach (var value1 in first.Distinct())
        {
            foreach (var value2 in other)
            {
                if (value1.Equals(value2))
                {
                    result.Add(value1);
                }
                break;
            }
        }
        return result;
    }

    public static IEnumerable<T> Unionn<T>(this IEnumerable<T> first, IEnumerable<T> other) where T : IEqualityComparer<T>
    {
        List<T> result = new List<T>();
        foreach (var value1 in first.Distinct())
        {
            result.Add(value1);
        }
        foreach (var value2 in other.Distinct())
        {
            if (result.Contains(value2))
            {
                continue;
            }
            result.Add(value2);
        }
        return result;
    }

    public static IEnumerable<T> Complementt<T>(this IEnumerable<T> first, IEnumerable<T> universe) where T : IEqualityComparer<T>
    {
        foreach (var element in universe)
        {
            if (!first.Contains(element))
            {
                yield return element;
            }
        }
    }

}
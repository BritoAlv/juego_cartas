namespace Poker;
internal static class Comb
{
    /// <summary>
    /// Generate all combinations of specified size of objects in an IEnumerable, (needs to be be finite)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objects"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static IEnumerable<IEnumerable<T>> generate_combinaciones<T>(this IEnumerable<T> objects, int size)
    {
        List<T> Objects = objects.ToList();
        if (Objects.Count <= size)
        {
            yield return Objects.AsEnumerable();
            yield break;
        }
        foreach (var solution in combinacion_optimized(new bool[Objects.Count], 0, 0))
        {
            yield return solution;
        }
        IEnumerable<IEnumerable<T>> combinacion_optimized(bool[] tomadas, int cant_tomadas, int lower)
        {
            if (cant_tomadas == size)
            {
                var result = new List<T>(size);
                for (int i = 0; i < Objects.Count; i++)
                {
                    if (tomadas[i])
                    {
                        result.Add(Objects[i]);
                    }
                }
                yield return result.AsEnumerable();
            }
            else
            {
                for (int i = lower; i < Objects.Count; i++)
                {
                    tomadas[i] = true;
                    foreach(var solution in combinacion_optimized(tomadas, cant_tomadas + 1, i + 1))
                    {
                        yield return solution;
                    }
                    tomadas[i] = false;
                }
            }
        }
        /*
        In this solution I use a boolean mask for the backtracking.
        */
    }
}

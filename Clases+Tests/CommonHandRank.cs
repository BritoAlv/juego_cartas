using Poker;
public static class Common_Hand_Rank
{
    public static int CommonRanker(HandRank common_rank, Hand A, Hand B)
    {
        switch (common_rank)
        {
            case HandRank.EscaleraReal:
                return 0;
            case HandRank.EscaleraColor:
                return RankByEscalera(A.Cards, B.Cards);
            case HandRank.CuatroIguales:
                return RankByFourEquals(A.Cards, B.Cards);
            case HandRank.Full:
                return RankByFull(A.Cards, B.Cards);
            case HandRank.Color:
                return RankByColor(A.Cards, B.Cards);
            case HandRank.Escalera:
                return RankByEscalera(A.Cards, B.Cards);
            case HandRank.Trio:
                return RankByTrio(A.Cards, B.Cards);
            case HandRank.DosParejas:
                return RankByTwoParejas(A.Cards, B.Cards);
            case HandRank.Pareja:
                return RankByPareja(A.Cards, B.Cards);
            default:
                return RankByHighCard(A.Cards, B.Cards);
        }
    }
    private static int RankByFull(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return RankByTrio(A, B);
    }
    private static int RankByFourEquals(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        CardValue Transform(IEnumerable<Card> C)
        {
            return C.GroupBy(x => x.Value).Where(x => x.Count() == 4).First().First().Value;
        }
        var result = Transform(A).CompareTo(Transform(B));
        if (result == 0)
        {
            return RankByHighCard(A, B);
        }
        return result;
    }

    private static int RankByEscalera(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return RankByHighCard(A, B);
    }

    private static int RankByColor(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        return RankByHighCard(A, B);
    }
    private static int RankByTrio(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        CardValue Transform(IEnumerable<Card> C)
        {
            return C.GroupBy(x => x.Value).Where(x => x.Count() == 3).First().First().Value;
        }
        var result = Transform(A).CompareTo(Transform(B));
        if (result == 0)
        {
            return RankByPareja(A, B);
        }
        return result;
    }
    private static int RankByTwoParejas(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        IEnumerable<CardValue> Transform(IEnumerable<Card> C)
        {
            return C.GroupBy(x => x.Value).Where(x => x.Count() == 2).Select(x => x.First().Value).OrderDescending();
        }
        var zipped = Transform(A).Zip(Transform(B));
        foreach (var tuple in zipped)
        {
            if (tuple.First > tuple.Second)
            {
                return 1;
            }
            else if (tuple.Second > tuple.First)
            {
                return -1;
            }
            else
            {
                continue;
            }
        }
        return RankByHighCard(A, B);
    }
    private static int RankByPareja(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        CardValue Transform(IEnumerable<Card> C)
        {
            return C.GroupBy(x => x.Value).Where(x => x.Count() == 2).First().First().Value;
        }
        var result = Transform(A).CompareTo(Transform(B));
        if (result == 0)
        {
            return RankByHighCard(A, B);
        }
        return result;
    }
    private static int RankByHighCard(IEnumerable<Card> A, IEnumerable<Card> B)
    {
        IEnumerable<Card> Transform(IEnumerable<Card> C)
        {
            return C.OrderByDescending(x => x.Value);
        }
        var zipped = Transform(A).Zip(Transform(B));
        foreach (var tuple in zipped)
        {
            if (tuple.First.Value > tuple.Second.Value)
            {
                return 1;
            }
            else if (tuple.Second.Value > tuple.First.Value)
            {
                return -1;
            }
            else
            {
                continue;
            }
        }
        return 0;
    }
}
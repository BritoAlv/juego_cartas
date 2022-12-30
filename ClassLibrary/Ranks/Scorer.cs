namespace Poker;
public class Scorer
{
    private List<Rank> Rankings;
    public Scorer(params Rank[] some_ranks)
    {
        Rankings = new List<Rank>();
        Rankings.Add(new EscaleraColorRank("escaleracolor"));
        Rankings.Add(new FourKindRank("cuatrotipo"));
        Rankings.Add(new FullRank("full"));
        Rankings.Add(new EscaleraRank("escalera"));
        Rankings.Add(new ColorRank("color"));
        Rankings.Add(new TrioRank("trio"));
        Rankings.Add(new ThreePairRank("tresparejas"));
        Rankings.Add(new TwoPairRank("dosparejas"));
        Rankings.Add(new PairRank("pareja"));
        Rankings.Add(new CartaAltaRank("cartaalta"));
        foreach (var rank in some_ranks)
        {
            Rankings.Add(rank);
        }
        Rankings = Rankings.OrderByDescending(x => x.Priority).ToList();
    }
    public void Add_Rank(Rank rank)
    {
        if (Rankings.Any(x => x.Priority == rank.Priority || x.Id == rank.Id ))
        {
            throw new Exception("No se puede añadir un rango igual que los que están");
        }
        Rankings.Add(rank);
        Rankings = Rankings.OrderByDescending(x => x.Priority).ToList();
    }
    public void Delete_Rank(string id)
    {
        Rankings = Rankings.Where(x => x.Id != id).ToList();
    }
    public HandRank GetHandRank(IEnumerable<Card> cards)
    {
        Rank a = Rankings.First(x => x.HasThisRank(cards));
        return new HandRank(a.Id, a.Priority);
    }
    public int Common_Ranker(HandRank rank, IEnumerable<Card> A, IEnumerable<Card> B)
    {
        var the_rank = Rankings.Where(x => x.Priority == rank.Priority).First();
        return the_rank.CommonRanker(A,B);
    }    
}
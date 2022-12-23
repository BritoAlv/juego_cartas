namespace Poker;
public class Scorer
{
    private List<Rank> Rankings;
    public Scorer(params Rank[] some_ranks)
    {
        Rankings = new List<Rank>();
        Rankings.Add(new EscaleraColorRank("Escalera-Color"));
        Rankings.Add(new FourKindRank("Cuatro Tipo"));
        Rankings.Add(new FullRank("Full"));
        Rankings.Add(new EscaleraRank("Escalera"));
        Rankings.Add(new ColorRank("Color"));
        Rankings.Add(new TrioRank("Un Trio"));
        Rankings.Add(new ThreePairRank("Tres Parejas"));
        Rankings.Add(new TwoPairRank("Dos Parejas"));
        Rankings.Add(new PairRank("Una Pareja"));
        Rankings.Add(new CartaAltaRank("Carta Alta"));
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
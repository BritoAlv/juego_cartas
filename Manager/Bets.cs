namespace Game;
internal class Bet
{
    internal Bet(IEnumerable<Player> participants)
    {
        Bets = new Dictionary<Ideable, List<int>>();
        foreach (var participant in participants)
        {
            Bets[participant] = new List<int>();
        }
    }
    public Dictionary<Ideable, List<int>> Bets { get; private set; }

    internal void Apostar(Player A, int dinero)
    {
        Bets[A].Add(dinero);
    }

    internal List<int> this[Ideable index]
    {
        get { return Bets[index]; }
    }
    internal int Get_Dinero_Apostado(Player A)
    {
        return Bets[A].Sum();
    }

    internal int Get_Last_Apuesta(Player A)
    {
        return Bets[A].Last();
    }

    internal int Get_Dinero_Total_Apostado()
    {
        return Bets.Values.Aggregate(0, (actual, next) => actual + next.Sum());
        // other way of do the same process. 
        // return Bets.Values.Select(x => x.Sum()).Sum();
    }


}
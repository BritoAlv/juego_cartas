namespace Poker;
/// <summary>
/// This represent a basic computer player who knows by default how to Bet.
/// </summary>
public class Basic_Computer_Player : Player
{
    public Basic_Computer_Player(string id, int dinero) : base(id, dinero)
    {
    }

    HandRank? current { get; set; }
    public override int realizar_apuesta(IGlobal_Contexto contexto)
    {
        // bet enough in an aggressive way.
        if (Hand.rank.Priority >= 5) // has something that could be nice
        {
            return Math.Min(this.Dinero, contexto.PlayerManager.Get_Active_Players(2).Select(x => x.Dinero).Order().First()*3);
        }
        var apuesta = contexto.PlayerManager.Get_Active_Players(2).Select(x => contexto.Ronda_Contexto.Apuestas.Get_Dinero_Apostado(x)).Max();
        if (apuesta > 0)
        {
            return Math.Min(apuesta, this.Dinero);
        }
        if (Hand.rank.Priority >= 2) // computer player is waiting for something.
        {
            return this.Dinero / 10 == 0 ? 1 : this.Dinero / 10;
        }
        return 1;
    }
    public override IDecision parse_decision(IGlobal_Contexto contexto)
    {
        if (current is null)
        {
            current = this.Hand.rank;
        }
        if (contexto.Ronda_Contexto.Apuestas.Get_Max_Apuesta() > this.Dinero*3)
        {
            return new Abandonar();
        }
        // if my hand gets better bet.
        if (current.Priority > 3 && current.Priority < this.Hand.rank.Priority)
        {
            current = this.Hand.rank;
            return new Apostar(this);
        }
        // Decision of the computer player needs to depend on actual state of the game.
        // computer has an aggressive hand or computer has hope of win, so keeps betting.
        if (is_aggresive(this.Hand) || this.Hand.rank.Priority >= 3)
        {
            return new Apostar(this);
        }

        // make a decision that depends on the actual state of the game.


        if (contexto.Ronda_Contexto.Apuestas.Puede_Pasar(this))
        {
            return new Pasar();
        }

        else
        {
            return new Abandonar();
        }
    }

    /// <summary>
    // Determine if a hand is aggressive, this means that the player should bet.
    /// </summary>
    /// <param name="hand"></param>
    /// <returns></returns>
    private bool is_aggresive(Hand hand)
    {
        if (hand.Cards.Select(x => (int)x.Value).Sum() / hand.Cards.Count() > 9)
        {
            return true;
        }
        return false;
    }
}
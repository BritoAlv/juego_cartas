using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Poker;
public sealed class Test_Player : Computer_Player
{
    public Test_Player(string id, int dinero) : base(id, dinero)
    {
    }

    public override IDecision parse_decision(Contexto contexto)
    {
        return new Apostar(this);
    }

    public override int realizar_apuesta(Contexto contexto)
    {
        var mayor_dinero = contexto.Active_Players.Select(x => contexto.Apuestas.Get_Dinero_Apostado(x)).Max();
        if(Hand.rank.Priority>2) return Math.Min(mayor_dinero, this.Dinero);
        int apuesta =1;
        if(mayor_dinero > this.Dinero/2){
            if(Hand.rank.Priority>=1) apuesta = this.Dinero/3;
            else apuesta = this.Dinero/10;
        }else{
            if(Hand.rank.Priority>=1) apuesta = this.Dinero/2;
            else apuesta = this.Dinero/10;
        }
        if(apuesta == 0) return 1;
        else return apuesta;
    }

    // public override int realizar_apuesta(Bet apuestas, IEnumerable<Player> Players, string info_apuesta)
    // {
    //     var rank = Hand.rank.Priority;
    //     if(rank==0) return (int)(this.Dinero/15);
    //     var apuesta = (int)Math.Floor(this.Dinero*(rank/4));
    //     return apuesta;
    // }
} 
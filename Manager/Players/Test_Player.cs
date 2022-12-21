using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Poker;
public sealed class Test_Player : Player
{
    public Test_Player(string id, int dinero) : base(id, dinero)
    {
    }

    public override int realizar_apuesta(Bet apuestas, IEnumerable<Player> Players, string info_apuesta)
    {
        var rank = Hand.rank.Priority;
        if(rank==0) return (int)(this.Dinero/15);
        var apuesta = (int)Math.Floor(this.Dinero*(rank/4));
        return apuesta;
    }
} 
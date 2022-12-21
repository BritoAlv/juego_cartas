using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Poker;
public sealed class Test_Best_Player : Player
{
    public Test_Best_Player(string id, int dinero) : base(id, dinero)
    {
    }

    public override int realizar_apuesta(Bet apuestas, IEnumerable<Player> Players, string info_apuesta)
    {
        var rank = Hand.rank.Priority;
        var mayor_dinero = Players.Select(x => apuestas.Get_Dinero_Apostado(x)).Max();
        double apuesta =1;
        switch (mayor_dinero)
        {
            case < 10:{
                if(rank>1) apuesta = this.Dinero*0.8;
                else if(rank==1) apuesta =  this.Dinero*0.3;
                else apuesta = this.Dinero*0.7;
                break;
            }
            case > 50:{
                if(rank>2) apuesta = this.Dinero*0.3;
                else if(rank>=1) apuesta = this.Dinero*0.4;
                apuesta = this.Dinero*0.1;
                break;
            }
            default: {
                if(rank==1) apuesta = this.Dinero*0.3;
                else if(rank>1) apuesta = this.Dinero*0.2;
                apuesta = this.Dinero*0.1;
                break;
            }
        }
        

        throw new ArgumentException();
    }
} 
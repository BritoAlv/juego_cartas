namespace Poker;

public class Abandonar : IDecision
{
    public string Id => "Abandonar";
    public string Help => "No seguir jugando en esta ronda, que te lo impide?";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        contexto.Ronda_Contexto.Participants = contexto.Ronda_Contexto.Participants.Where(x => x.Id != player.Id);
        return true;
    }
}
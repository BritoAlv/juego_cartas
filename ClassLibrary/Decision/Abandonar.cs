namespace Poker;
internal sealed class Abandonar : IDecision
{
    public string Id => "Abandonar";
    public string Help => "No seguir jugando en esta ronda, que te lo impide?";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        contexto.PlayerManager.Filtro_Ronda.Add(x => (x.Id != player.Id));
        return true;
    }
}
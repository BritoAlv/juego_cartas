namespace Poker;
/// <summary>
/// This captures the concept of a decision.
/// </summary>
public interface IDecision
{
    /// <summary>
    /// An Id to identify distinct decisions.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// Given a player, this will try to execute the decision of the player, returning if fails or not the player.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    bool DoDecision(Player player, IGlobal_Contexto contexto);
    /// <summary>
    /// A string message in case the Player doesn't know how to put this decision in practice.
    /// </summary>
    string Help { get; }
}
/// <summary>
/// Default Decision which is the wrong one.
/// </summary>
internal sealed class InvalidDecision : IDecision
{
    public string Id => "InvalidDecision";
    public bool DoDecision(Player player, IGlobal_Contexto contexto)
    {
        return false;
    }
    public string Help => "Invalid";
}

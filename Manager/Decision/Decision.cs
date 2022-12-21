namespace Poker;
public interface IDecision
{
    string Id { get; }
    bool DoDecision(Player player, Contexto contexto);
    string Help { get; }
}




internal class InvalidDecision : IDecision
{
    public string Id => "InvalidDecision";
    public bool DoDecision(Player player, Contexto contexto)
    {
        return false;
    }

    public string Help => "Invalid";
}

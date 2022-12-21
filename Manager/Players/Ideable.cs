namespace Poker;
/// <summary>
/// Sometimes from a Player we only need its Id. 
/// </summary>
public interface Ideable
{
    string Id { get; }
}

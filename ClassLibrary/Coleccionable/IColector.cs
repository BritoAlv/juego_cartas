namespace Poker;
public interface IColector
{
    List<string> get_efectos { get; }
    void add_efecto(string efecto);
    string remove_efecto();
}

namespace Poker;
public partial class Func_Generator
{
    private Func<IEnumerable<Card>, IEnumerable<Card?>> get_card_func(UnaryDescriptionArgument unary)
    {
        var identifier = unary.Objeto.Text;
        switch (identifier)
        {
            case "Valor":
                return Card_Func_Valor(unary.Description.Text);
            case "Suit":
                return Card_Func_Suit(unary.Description.Text);
            default:
                return x => new List<Card?>();
        }
    }



    private Func<IEnumerable<Player>, IEnumerable<Player?>> get_player_func(UnaryDescriptionArgument unary)
    {
        var identifier = unary.Objeto.Text;
        switch (identifier)
        {
            case "Jugador":
                return Player_Func_Jugador(unary.Description.Text);
            case "Dinero":
                return Player_Func_Dinero(unary.Description.Text);
            case "Apuesta":
                return Player_Func_Apuesta(unary.Description.Text);
            default:
                return x => new List<Player?>();
        }
    }

}
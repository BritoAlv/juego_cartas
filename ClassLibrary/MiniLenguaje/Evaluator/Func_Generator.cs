namespace Poker;
public static class Func_Generator
{
    internal static List<Func<IEnumerable<Card>, Card?>> GetCardFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<Card>, Card?>> result = new List<Func<IEnumerable<Card>, Card?>>();
        foreach (var argument in arguments.Descriptions)
        {
            if (argument is UnaryDescriptionArgument unary)
            {
                result.Add(get_card_func(unary));
            }
            if (argument is BinaryDescriptionArgument binary)
            {
                result.Add(get_card_func(binary));
            }
        }
        return result;
    }

    private static Func<IEnumerable<Card>, Card?> get_card_func(BinaryDescriptionArgument binary)
    {
        if (binary.Operador.Text == "&&")
        {
            return x => x.OrderByDescending(x => x.Value).FirstOrDefault();
        }
        else if (binary.Operador.Text == "||")
        {
            
        }
        return x => null;
    }

    private static Func<IEnumerable<Card>, Card?> get_card_func(UnaryDescriptionArgument unary)
    {
        throw new NotImplementedException();
    }

    internal static List<Func<IEnumerable<Player>, Player?>> GetPlayerFunction(LiteralArguments arguments)
    {
        List<Func<IEnumerable<Player>, Player?>> result = new List<Func<IEnumerable<Player>, Player?>>();
        foreach (var argument in arguments.Descriptions)
        {
            if (argument is UnaryDescriptionArgument unary)
            {
                result.Add(get_player_func(unary));
            }
            if (argument is BinaryDescriptionArgument binary)
            {
                result.Add(get_player_func(binary));
            }
        }
        return result;
    }

    private static Func<IEnumerable<Player>, Player?> get_player_func(BinaryDescriptionArgument binary)
    {
        throw new NotImplementedException();
    }
    private static Func<IEnumerable<Player>, Player?> get_player_func(UnaryDescriptionArgument unary)
    {
        switch (unary.Objeto.Text)
        {
            case "Jugador":
                return x => x.FirstOrDefault(m => m.Id == unary.Description.Text);
            case "Dinero":
                return x => x.OrderByDescending(m => m.Dinero).FirstOrDefault();
            default:
                return x => null;
        }
    }
}
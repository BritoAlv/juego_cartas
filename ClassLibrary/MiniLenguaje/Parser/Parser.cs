namespace Poker;
public class Parser
{
    public Parser(List<Token> tokens)
    {
        Tokens = tokens;
    }
    public List<Token> Tokens { get; }
    int position = 0;
    Token Current
    {
        get
        {
            if (position < Tokens.Count)
            {
                return Tokens[position];
            }
            return new SyntaxToken(Tipo.Wrong, "\0");
        }
    }
    public Token LookAhead(int distance)
    {
        if (position + distance < Tokens.Count)
        {
            return Tokens[position + distance];
        }
        return Current;
    }
    Token Match(Tipo tipo)
    {
        if (Current.Tipo == tipo)
        {
            var current = Current;
            position++;
            return current;
        }
        return new SyntaxToken(Tipo.Wrong, "\0");
    }

    // public API Parse.
    public Accion Parse()
    {
        var verb_action = LookAhead(1);
        return ParseAction(verb_action.Text);
    }

    private Accion ParseAction(string v)
    {
        var open_parenthesis = Match(Tipo.ParéntesisAbierto);
        var signature = Match(Tipo.Accion);
        if (v == "$añadircarta")
        {
            return new AñadirCarta(open_parenthesis, signature, ParseArgument<Card>(), ParseArgument<Player>(), Match(Tipo.ParéntesisCerrado));
        }
        if (v == "$robarcarta")
        {
            return new RobarCarta(open_parenthesis, signature, ParseArgument<Card>(), ParseArgument<Player>(), Match(Tipo.ParéntesisCerrado));
        }
        if (v == "$banearjugador")
        {
            return new BanearJugador(open_parenthesis, signature, ParseArgument<Player>(), Match(Tipo.ParéntesisCerrado));
        }
        throw new Exception($"El nombre de la acción {signature.Text} no se encontró entre los nombres predefinidos");
    }

    private IArgument<T> ParseArgument<T>() where T: IDescribable<T>, IEqualityComparer<T>
    {
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            var open = Current;
            position++;
            var find_T = ParseAction(LookAhead(1).Text);
            var closed = Match(Tipo.LLaveCerrada);
            position++;
            return (IArgument<T>)find_T;
        }
        else
        {
            return ParseLiteral<T>();
        }
    }
    private LiteralDescribe<T> ParseLiteral<T>() where T: IDescribable<T>, IEqualityComparer<T>
    {
        Token open_brace = Match(Tipo.CorcheteAbierto);
        var tokens_description = ParseDescriptionTokens(Tipo.CorcheteCerrado);
        Token closed_brace = Match(Tipo.CorcheteCerrado);
        return new LiteralDescribe<T>(open_brace, new LiteralArguments(tokens_description), closed_brace);
    }
    private List<Token> ParseDescriptionTokens(Tipo tipo)
    {
        List<Token> tokens_description = new List<Token>();
        while (Current.Tipo != tipo)
        {
            tokens_description.Add(Current);
            position++;
        }
        return tokens_description;
    }
}
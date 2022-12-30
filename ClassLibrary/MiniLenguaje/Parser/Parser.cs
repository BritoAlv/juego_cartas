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
    // differentiate between parse an action player or an action card.
    private Accion ParseAction(string v)
    {
        var open_parenthesis = Match(Tipo.ParéntesisAbierto);
        var signature = Match(Tipo.Accion);
        if (v.StartsWith("$añadircarta"))
        {
            return new AñadirCarta(open_parenthesis, signature, ParseArgumentCard(), ParseArgumentPlayer(), Match(Tipo.ParéntesisCerrado));
        }
        if (v.StartsWith("$robarcarta"))
        {
            return new RobarCarta(open_parenthesis, signature, ParseArgumentCard(), ParseArgumentPlayer(), Match(Tipo.ParéntesisCerrado));
        }
        throw new Exception("Un acción debe empezar especificando el tipo de retorno");
    }
    private LiteralDescribeCard ParseLiteralCard()
    {
        Token open_brace = Match(Tipo.CorcheteAbierto);
        var tokens_description = ParseDescriptionTokens(Tipo.CorcheteCerrado);
        Token closed_brace = Match(Tipo.CorcheteCerrado);
        return new LiteralDescribeCard(open_brace, new LiteralArguments(tokens_description), closed_brace);
    }
    private LiteralDescribePlayer ParseLiteralPlayer()
    {
        Token open_llave = Match(Tipo.LLaveAbierta);
        var tokens_description = ParseDescriptionTokens(Tipo.LLaveCerrada);
        Token closed_llave = Match(Tipo.LLaveCerrada);
        return new LiteralDescribePlayer(open_llave, new LiteralArguments(tokens_description), closed_llave);
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
    private IArgument<Player> ParseArgumentPlayer()
    {
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            var open_llave = Match(Tipo.LLaveAbierta);
            var find_player = ParseAction(LookAhead(1).Text);
            var closed_llave = Match(Tipo.LLaveCerrada);
            return (IArgument<Player>)find_player;
        }
        else
        {
            return ParseLiteralPlayer();
        }
    }
    private IArgument<Card> ParseArgumentCard()
    {
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            var open_corchete = Match(Tipo.CorcheteAbierto);
            var find_card = ParseAction(LookAhead(1).Text);
            var closed_corchete = Match(Tipo.CorcheteCerrado);
            return (IArgument<Card>)find_card;
        }
        else
        {
            return ParseLiteralCard();
        }
    }
}
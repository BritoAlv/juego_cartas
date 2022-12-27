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

    // the actions defined here will be predefined by myself.
    public object Parse()
    {
        var verb_action = LookAhead(1);
        switch (verb_action.Text)
        {
            case "$añadircarta":
                return ParseCard(verb_action.Text);
            default:
                throw new Exception();
        }
    }


    private FindObject<Card> ParseCard(string v)
    {
        switch (v)
        {
            case "$robarcarta":
                return ParseRobarCard();
            case "$añadircarta":
                return ParseAñadirCarta();
            default:
                return ParseLiteralCard();
        }
    }

    private FindObject<Card> ParseAñadirCarta()
    {
        var open_parenthesis = Match(Tipo.ParéntesisAbierto);
        var signature = Match(Tipo.Accion);
        var find_card = ParseArgumentCard();
        var find_player = ParseArgumentPlayer();
        var closed_parenthesis = Match(Tipo.ParéntesisCerrado);
        return new CompoundActionCard(open_parenthesis, signature, find_card, find_player, closed_parenthesis);

    }

    private FindObject<Player> ParsePlayer(string v)
    {
        switch (v)
        {
            case "banear":
                return ParseBanearPlayer();
            default:
                return ParseLiteralPlayer();
        }
    }

    private FindObject<Card> ParseLiteralCard()
    {
        Token open_brace = Match(Tipo.CorcheteAbierto);
        List<Token> tokens_description = new List<Token>();
        while (Current.Tipo != Tipo.CorcheteCerrado)
        {
            tokens_description.Add(Current);
            position++;
        }
        Token closed_brace = Match(Tipo.CorcheteCerrado);
        return new LiteralDescribeCard(open_brace, tokens_description, closed_brace);
    }

    private FindObject<Player> ParseLiteralPlayer()
    {
        Token open_llave = Match(Tipo.LLaveAbierta);
        List<Token> tokens_description = new List<Token>();
        while (Current.Tipo != Tipo.LLaveCerrada)
        {
            tokens_description.Add(Current);
            position++;
        }
        Token closed_llave = Match(Tipo.LLaveCerrada);
        return new LiteralDescribePlayer(open_llave, tokens_description, closed_llave);
    }

    private FindObject<Player> ParseBanearPlayer()
    {
        throw new NotImplementedException();
    }
    private FindObject<Card> ParseRobarCard()
    {
        var open_parenthesis = Match(Tipo.ParéntesisAbierto);
        var signature = Match(Tipo.Accion);
        FindObject<Card> find_card = ParseArgumentCard();
        FindObject<Player> find_player = ParseArgumentPlayer();
        var closed_parenthesis = Match(Tipo.ParéntesisCerrado);
        return new CompoundActionCard(open_parenthesis, signature, find_card, find_player, closed_parenthesis);
    }

    private FindObject<Player> ParseArgumentPlayer()
    {
        FindObject<Player> find_player;
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            var open_llave = Match(Tipo.LLaveAbierta);
            find_player = ParsePlayer(LookAhead(1).Text);
            var closed_llave = Match(Tipo.LLaveCerrada);
        }
        else
        {
            find_player = ParsePlayer("");
        }

        return find_player;
    }

    private FindObject<Card> ParseArgumentCard()
    {
        FindObject<Card> find_card;
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            var open_corchete = Match(Tipo.CorcheteAbierto);
            find_card = ParseCard(LookAhead(1).Text);
            var closed_corchete = Match(Tipo.CorcheteCerrado);
        }
        else
        {
            find_card = ParseCard("");
        }

        return find_card;
    }
}
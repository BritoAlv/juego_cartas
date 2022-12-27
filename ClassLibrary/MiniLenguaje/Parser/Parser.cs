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

    // first differentiate between parse a card action or a player action. 
    public Iprintable Parse()
    {
        var verb_action = LookAhead(1);
        switch (verb_action.Text)
        {
            case "$añadircarta":
            case "$robarcarta":
            case "$banearjugador":
                return ParseAction(verb_action.Text);
            default:
                throw new Exception();
        }
    }
    private CompoundAction ParseAction(string v)
    {
        switch (v)
        {
            case "$robarcarta":
            case "$añadircarta":
                return ParseCardAction();
            case "$banearjugador":
                return ParsePlayerAction();
            default:
                throw new Exception();
        }
    }
    private ActionCard ParseCardAction()
    {
        var open_parenthesis = Match(Tipo.ParéntesisAbierto);
        var signature = Match(Tipo.Accion);
        var find_card = ParseArgumentCard();
        var find_player = ParseArgumentPlayer();
        var closed_parenthesis = Match(Tipo.ParéntesisCerrado);
        return new ActionCard(open_parenthesis, signature, find_card, find_player, closed_parenthesis);
    }

    private ActionPlayer ParsePlayerAction()
    {
        var open_parenthesis = Match(Tipo.ParéntesisAbierto);
        var signature = Match(Tipo.Accion);
        var find_card = ParseArgumentCard();
        var find_player = ParseArgumentPlayer();
        var closed_parenthesis = Match(Tipo.ParéntesisCerrado);
        return new ActionPlayer(open_parenthesis, signature, find_card, find_player, closed_parenthesis);
    }
    
    private LiteralDescribeCard ParseLiteralCard()
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




    private LiteralDescribePlayer ParseLiteralPlayer()
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

    private IFindPlayer ParseArgumentPlayer()
    {
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            var open_llave = Match(Tipo.LLaveAbierta);
            var find_player = ParsePlayerAction();
            var closed_llave = Match(Tipo.LLaveCerrada);
            return find_player;
        }
        else
        {
            return ParseLiteralPlayer();
        }
    }

    private IFindCard ParseArgumentCard()
    {
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            var open_corchete = Match(Tipo.CorcheteAbierto);
            var find_card = ParseCardAction();
            var closed_corchete = Match(Tipo.CorcheteCerrado);
            return find_card;
        }
        else
        {
            return ParseLiteralCard();
        }
    }
}
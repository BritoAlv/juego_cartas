namespace Poker;
public class Parser
{
    public Parser(List<Token> tokens, IGlobal_Contexto contexto)
    {
        Tokens = tokens;
        Contexto = contexto;
    }
    public List<Token> Tokens { get; }
    public IGlobal_Contexto Contexto { get; }

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
    public Token Match(Tipo tipo)
    {
        if (Current.Tipo == tipo)
        {
            var current = Current;
            position++;
            return current;
        }
        return new SyntaxToken(Tipo.Wrong, "\0");
    }
    public IArgument<T> ParseArgument<T>() where T : IDescribable<T>, IEqualityComparer<T>
    {
        if (LookAhead(1).Tipo == Tipo.ParéntesisAbierto)
        {
            position++;
            var find_T = Contexto.factory.CreateAction(LookAhead(1).Text, this);
            position++;
            return (IArgument<T>)find_T;
        }
        else
        {
            return ParseLiteral<T>();
        }
    }
    private LiteralDescribe<T> ParseLiteral<T>() where T : IDescribable<T>, IEqualityComparer<T>
    {
        Token open = Current;
        Tipo opposite = Get_Opposite(open.Text);
        position++;
        var tokens_description = ParseDescriptionTokens(opposite);
        Token closed = Current;
        position++;
        return new LiteralDescribe<T>(open, new LiteralArguments(tokens_description), closed);
    }

    private Tipo Get_Opposite(string text)
    {
        if (text == "[")
        {
            return Tipo.CorcheteCerrado;
        }
        if (text == "(")
        {
            return Tipo.ParéntesisCerrado;
        }
        if (text == "{")
        {
            return Tipo.LLaveCerrada;
        }
        if (text == "¿")
        {
            return Tipo.ParéntesisCerrado;
        }
        return Tipo.Wrong;
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
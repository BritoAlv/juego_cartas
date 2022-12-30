namespace Poker;
public class Lexer
{
    int position = 0;
    char Current
    {
        get
        {
            if (position <= Line.Length - 1)
            {
                return Line[position];
            }
            return '\0';
        }
    }
    public string LookAhead(int distance)
    {
        string result = "";
        for (int i = 0; i < distance && (position + i < Line.Length); i++)
        {
            result = result + Line[position + i];
        }
        return result;
    }
    public Lexer(string line)
    {
        Line = line;
    }
    public string Line { get; }
    public List<Token> Lex()
    {
        var result = new List<Token>();
        while (Current != '\0')
        {
            if (Current == ' ')
            {
                position++;
            }
            else if (Current == '(')
            {
                result.Add(new SyntaxToken(Tipo.ParéntesisAbierto, "("));
                position++;
            }
            else if (Current == ')')
            {
                result.Add(new SyntaxToken(Tipo.ParéntesisCerrado, ")"));
                position++;
            }
            else if (Current == '{')
            {
                result.Add(new SyntaxToken(Tipo.LLaveAbierta, "{"));
                position++;
            }
            else if (Current == '}')
            {
                result.Add(new SyntaxToken(Tipo.LLaveCerrada, "}"));
                position++;
            }
            else if (Current == '[')
            {
                result.Add(new SyntaxToken(Tipo.CorcheteAbierto, "["));
                position++;
            }
            else if (Current == ']')
            {
                result.Add(new SyntaxToken(Tipo.CorcheteCerrado, "]"));
                position++;
            }
            else if (Current == '¿')
            {
                result.Add(new SyntaxToken(Tipo.QuestionAbierta, "¿"));
                position++;
            }
            else if (Current == '?')
            {
                result.Add(new SyntaxToken(Tipo.QuestionCerrada, "?"));
                position++;
            }
            else if (LookAhead(2) == "&&")
            {
                result.Add(new SyntaxToken(Tipo.And, "&&"));
                position = position + 2;
            }
            else if (LookAhead(2) == "||")
            {
                result.Add(new SyntaxToken(Tipo.And, "||"));
                position = position + 2;
            }
            else if (Current == '$')
            {
                position++;
                var text = "$" + LexWord();
                result.Add(new ActionToken(Tipo.Accion, text));
            }
            else if (char.IsAsciiLetterUpper(Current))
            {
                var text = LexWord();
                if (text == text.ToUpper())
                {
                    result.Add(new ObjetoToken(Tipo.Nombre, text));
                    continue;
                }
                result.Add(new ObjetoToken(Tipo.Objeto, text));
            }
            else if (char.IsAsciiLetterLower(Current) || char.IsAsciiDigit(Current) || Current == '>' || Current == '<')
            {
                result.Add(new DescriptionToken(Tipo.Descripcion, LexDescription()));
            }
            else
            {
                position++;
            }
        }
        return result;
    }
    string LexWord()
    {
        var text = "";
        while (char.IsLetter(Current) || Current == '_')
        {
            text = text + Current;
            position++;
        }
        return text;
    }
    string LexDescription()
    {
        var text = "";
        while (char.IsLetterOrDigit(Current) || Current == '>' || Current == '<')
        {
            text = text + Current;
            position++;
        }
        return text.TrimEnd().TrimStart();
    }
}
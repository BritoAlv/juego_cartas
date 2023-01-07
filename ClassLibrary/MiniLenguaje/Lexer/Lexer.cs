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
            if (Current == '(')
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
            else if (Current == '¿')
            {
                result.Add(new SyntaxToken(Tipo.QuestionAbierta, "¿"));
                position++;
            }
            else if (Current == '^')
            {
                result.Add(new SyntaxToken(Tipo.Complemento, "^"));
                position++;
            }
            else if (Current == '?')
            {
                result.Add(new SyntaxToken(Tipo.QuestionCerrada, "?"));
                position++;
            }
            else if (Current == '!')
            {
                result.Add(new SyntaxToken(Tipo.ThirdOption, "!"));
                position++;
            }
            else if (LookAhead(2) == "=>")
            {
                result.Add(new SyntaxToken(Tipo.Implies, "=>"));
                position = position + 2;
            }
            else if (LookAhead(2) == "&&")
            {
                result.Add(new SyntaxToken(Tipo.And, "&&"));
                position = position + 2;
            }
            else if (LookAhead(2) == "||")
            {
                result.Add(new SyntaxToken(Tipo.Or, "||"));
                position = position + 2;
            }
            else if (LookAhead(2) == "if")
            {
                result.Add(new SyntaxToken(Tipo.IF, "if"));
                position = position + 2;
            }
            else if (Current == '$')
            {
                position++;
                var text = "$" + Lex_Word();
                result.Add(new ActionToken(Tipo.Accion, text));
            }
            else if (Current == '#')
            {
                position++;
                var text = "#" + Lex_Word();
                result.Add(new DescriptionToken(Tipo.Argumento, text));
            }
            else if (char.IsAsciiLetterUpper(Current))
            {
                /*
                Implicitly, Here is applied a specific syntax rule of the language after an Tipo.Objeto comes an Tipo.Descripción
                */
                var text = Lex_Word();
                result.Add(new ObjetoToken(Tipo.Objeto, text));
                position++;
                result.Add(new DescriptionToken(Tipo.Descripcion, Lex_Word()));
            }
            else
            {
                position++;
            }
        }
        return result;
    }

    string Lex_Word()
    {
        /*
        Implicitly is applied a specific syntax rule of the language, it's that we do not allow spaces between words.
        */
        var text = "";
        while(Current != ' ')
        {
            text = text + Current;
            position++;
        }
        return text;
    }
}
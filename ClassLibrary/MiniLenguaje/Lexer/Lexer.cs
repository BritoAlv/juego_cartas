namespace Poker;
public class Lexer
{
    static HashSet<char> syntax_token = new HashSet<char>()
    {
        '{', '}', '(', ')', '!', '^', '#'
    };
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
                result.Add(new Token(Tipo.ParéntesisAbierto, "("));
                position++;
            }
            else if (Current == ')')
            {
                result.Add(new Token(Tipo.ParéntesisCerrado, ")"));
                position++;
            }
            else if (Current == '{')
            {
                result.Add(new Token(Tipo.LLaveAbierta, "{"));
                position++;
            }
            else if (Current == '}')
            {
                result.Add(new Token(Tipo.LLaveCerrada, "}"));
                position++;
            }
            else if (Current == '¿')
            {
                result.Add(new Token(Tipo.QuestionAbierta, "¿"));
                position++;
            }
            else if (Current == '^')
            {
                result.Add(new Token(Tipo.Complemento, "^"));
                position++;
            }
            else if (Current == '?')
            {
                result.Add(new Token(Tipo.QuestionCerrada, "?"));
                position++;
            }
            else if (Current == '!')
            {
                result.Add(new Token(Tipo.ThirdOption, "!"));
                position++;
            }
            else if (LookAhead(2) == "=>")
            {
                result.Add(new Token(Tipo.Implies, "=>"));
                position = position + 2;
            }
            else if (LookAhead(2) == "&&")
            {
                result.Add(new Token(Tipo.And, "&&"));
                position = position + 2;
            }
            else if (LookAhead(2) == "||")
            {
                result.Add(new Token(Tipo.Or, "||"));
                position = position + 2;
            }
            else if (Current == '$')
            {
                position++;
                var text = "$" + Lex_Word();
                if (text == "$if")
                {
                    result.Add(new Token(Tipo.IF, "$if"));
                }
                else if (text == "$while")
                {
                    result.Add(new Token(Tipo.While, "$while"));
                }
                else
                {
                result.Add(new Token(Tipo.Accion, text));
                }
            }
            else if (Current == '#')
            {
                position++;
                var text = Lex_Word();
                result.Add(new Token(Tipo.Argumento, text));
            }
            else if (char.IsAsciiLetterUpper(Current))
            {
                /*
                Implicitly, Here is applied a specific syntax rule of the language after an Tipo.Objeto comes an Tipo.Descripción
                */
                var text = Lex_Word();
                result.Add(new Token(Tipo.Objeto, text));
                position++;
                result.Add(new Token(Tipo.Descripcion, Lex_Word()));
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
        while(Current != ' ' && Current != '\t' && Current != '\n' && !syntax_token.Contains(Current))
        {
            text = text + Current;
            position++;
        }
        return text;
    }
}
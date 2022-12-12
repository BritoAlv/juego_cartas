using System.Collections;

namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// Read the line of code introduced by the user and convert it to tokens.
    /// Lexer is like an Enumerator. It's use is restricted to the Parser.
    /// </summary>
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;
        private DiagnosticBag _diagnostics = new DiagnosticBag();
        public Lexer(string text)
        {
            this._text = text;
        }

        private char Current => Peek(0);
        private char LookAhead => Peek(1);


        private char Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _text.Length)
            {
                return '\0';
            }
            return _text[index];
        }

        private void Next()
        {
            _position++;
        }

        public DiagnosticBag Diagnostics => _diagnostics;

        public Token Lex()
        {
            if (_position >= _text.Length)
            {
                return new Token(Tipo.EndOfFileToken, _position, "\0", null);
            }
            var start = _position;
            if (char.IsDigit(Current))
            {
                while (char.IsDigit(Current))
                {
                    Next();
                }
                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                {
                    _diagnostics.ReportInvalidNumber(new TextSpan(start, length), _text, typeof(int));
                }
                return new Token(Tipo.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {

                while (char.IsWhiteSpace(Current))
                {
                    Next();
                }
                var length = _position - start;
                var text = _text.Substring(start, length);
                return new Token(Tipo.WhiteSpaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {
                while (char.IsLetter(Current))
                {
                    Next();
                }
                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeyWordKind(text);
                return new Token(kind, start, text, text);
            }

            switch (Current)
            {
                case '+':
                    return new Token(Tipo.PlusToken, _position++, "+", "Operador +");
                case '-':
                    return new Token(Tipo.MinusToken, _position++, "-", "Operador -");
                case '*':
                    return new Token(Tipo.StarToken, _position++, "*", "Operador *");
                case '/':
                    return new Token(Tipo.SlashToken, _position++, "/", "Operador /");
                case '(':
                    return new Token(Tipo.OpenParenthesisToken, _position++, "(", "Agrupador (");
                case ')':
                    return new Token(Tipo.CloseParenthesisToken, _position++, ")", "Agrupador )");

                case '&':
                    if (LookAhead == '&')
                    {
                        _position += 2;
                        return new Token(Tipo.AmpersandToken, start, "&&", "AND");
                    }
                    break;
                case '|':
                    if (LookAhead == '|')
                    {
                        _position += 2;
                        return new Token(Tipo.PipeToken, start, "||", "OR");
                    }
                    break;
                case '=':
                    if (LookAhead == '=')
                    {
                        _position += 2;
                        return new Token(Tipo.Equal, start, "==", "Equals");
                    }
                    else
                    {
                        _position += 1;
                        return new Token(Tipo.AsignacionToken, start, "=", "Asignacion =");
                    }
                case '!':
                    if (LookAhead == '=')
                    {
                        _position += 2;
                        return new Token(Tipo.Distinto, start, "!=", "NotEquals != ");
                    }
                    else
                    {
                        _position += 1;
                        return new Token(Tipo.BangToken, start, "!", "Negation !");
                    }
            }

            _diagnostics.ReportBadCharacter(_position, Current);
            return new Token(Tipo.BadToken, _position++, _text.Substring(_position - 1, 1), null);
        }

    }
}
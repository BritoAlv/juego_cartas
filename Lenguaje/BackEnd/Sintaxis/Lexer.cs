using System.Collections;

namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// Read the line of code introduced by the user and convert it to tokens.
    /// Lexer is like an Enumerator. It's use is restricted to the Parser.
    /// </summary>
    internal sealed class Lexer
    {
        private readonly SourceText _text;
        private int _position;
        private readonly DiagnosticBag _diagnostics = new DiagnosticBag();
        private int _start;
        private Tipo _kind;
        private object? _value;
        public Lexer(SourceText text)
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
            _start = _position;
            _kind = Tipo.BadToken;
            _value = null;
            switch (Current)
            {
                case '\0':
                    _kind = Tipo.EndOfFileToken;
                    break;
                case '+':
                    _kind = Tipo.PlusToken;
                    _position++;
                    break;
                case '-':
                    _kind = Tipo.MinusToken;
                    _position++;
                    break;
                case '*':
                    _kind = Tipo.StarToken;
                    _position++;
                    break;
                case '/':
                    _kind = Tipo.SlashToken;
                    _position++;
                    break;
                case '(':
                    _kind = Tipo.OpenParenthesisToken;
                    _position++;
                    break;
                case ')':
                    _kind = Tipo.CloseParenthesisToken;
                    _position++;
                    break;
                case '&':
                    if (LookAhead == '&')
                    {
                        _position += 2;
                        _kind = Tipo.AmpersandToken;
                    }
                    break;
                case '|':
                    if (LookAhead == '|')
                    {
                        _position += 2;
                        _kind = Tipo.PipeToken;
                    }
                    break;
                case '=':
                    if (LookAhead == '=')
                    {
                        _position += 2;
                        _kind = Tipo.Equal;
                        break;
                    }
                    else
                    {
                        _position += 1;
                        _kind = Tipo.AsignacionToken;
                        break;
                    }
                case '!':
                    if (LookAhead == '=')
                    {
                        _position += 2;
                        _kind = Tipo.Distinto;
                        break;
                    }
                    else
                    {
                        _position += 1;
                        _kind = Tipo.BangToken;
                        break;
                    }
                default:
                    if (char.IsDigit(Current))
                    {
                        ReadNumber();
                    }
                    else if (char.IsWhiteSpace(Current))
                    {
                        ReadWhiteSpace();
                    }
                    else if (char.IsLetter(Current))
                    {
                        ReadIdentifierKeyword();
                    }
                    else
                    {
                        _diagnostics.ReportBadCharacter(_position, Current);
                        _position++;
                    }
                    break;
            }
            var length = _position - _start;
            var text = SyntaxFacts.GetText(_kind);
            if (text == null)
            {
                text = _text.ToString(_start, length);
            }
            return new Token(_kind, _start, text, _value);
        }

        private void ReadIdentifierKeyword()
        {
            while (char.IsLetter(Current))
            {
                Next();
            }
            var length = _position - _start;
            var text = _text.ToString(_start, length);
            _kind = SyntaxFacts.GetKeyWordKind(text);
        }

        private void ReadWhiteSpace()
        {
            while (char.IsWhiteSpace(Current))
            {
                Next();
            }
            _kind = Tipo.WhiteSpaceToken;
        }

        private void ReadNumber()
        {
            while (char.IsDigit(Current))
            {
                Next();
            }
            var length = _position - _start;
            var text = _text.ToString(_start, length);
            if (!int.TryParse(text, out var value))
            {
                _diagnostics.ReportInvalidNumber(new TextSpan(_start, length), text, typeof(int));
            }
            _value = value;
            _kind = Tipo.NumberToken;
        }
    }
}
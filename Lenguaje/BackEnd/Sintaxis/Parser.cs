namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// Goal of the Parser is to generate an abstract syntax tree with the tokens provided by the lexer.
    /// </summary>
    internal sealed class Parser
    {
        private readonly Token[] _tokens;
        private int _position;
        private DiagnosticBag _diagnostics = new DiagnosticBag();

        public Parser(SourceText text)
        {
            var tokens = new List<Token>();
            var lexer = new Lexer(text);
            Token token;
            do
            {
                token = lexer.Lex();
                if
                (
                    token.tipo != Tipo.WhiteSpaceToken &&
                    token.tipo != Tipo.BadToken
                )
                {
                    tokens.Add(token);

                }
            } while (token.tipo != Tipo.EndOfFileToken);
            _diagnostics.AddRange(lexer.Diagnostics);
            _tokens = tokens.ToArray();
            Text = text;
        }

        public DiagnosticBag Diagnostics => _diagnostics;

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        private Token Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
            {
                return _tokens[_tokens.Length - 1];
            }
            return _tokens[index];
        }
        private Token Current => Peek(0);

        public SourceText Text { get; }

        /// <summary>
        /// Return the actual Token and move the Pointer.
        /// </summary>
        /// <returns></returns>
        private Token NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        /// <summary>
        /// Goal of this is that sometimes we expect that the Current is of type closed
        /// parenthesis, so we need to verify that.
        /// </summary>
        /// <param name="kind"></param>
        /// <returns></returns>
        private Token Match(Tipo kind)
        {
            if (Current.tipo == kind)
            {
                return NextToken();  // this returns the actual token, and move forward the pointer.
            }
            if (Current.tipo == Tipo.EndOfFileToken)
            {
                _diagnostics.ReportUnexpectedToken(new TextSpan(Current.Position, 0), Current.tipo, kind);
            }
            else
            {
                _diagnostics.ReportUnexpectedToken(Current.Span, Current.tipo, kind);
            }
            return new Token(kind, Current.Position, null, null);
        }

        /// <summary>
        /// The Method Parse, will return an Abstract Syntax Tree.
        /// </summary>
        /// <returns></returns>
        public NodoRoot Parse()
        {
            var expression = ParseExpresion();
            // notice that after parse the expression should be an endOfFileToken at the end.
            var endOfFileToken = Match(Tipo.EndOfFileToken);
            // The tree is made up of ExpressionSyntax objects,
            return new NodoRoot(Text, _diagnostics, expression, endOfFileToken);
        }
        public Expresion ParseExpresion()
        {
            return ParseAsignacion();
        }
        public Expresion ParseAsignacion()
        {
            if
            (
                Peek(0).tipo == Tipo.IdentifierToken &&
                Peek(1).tipo == Tipo.AsignacionToken
            )
            {
                var identificador = NextToken();
                var operador = NextToken();
                var right = ParseAsignacion();
                return new ExpresionAsignacion(identificador, operador, right);
            }
            return ParseExpresionBinaria();
        }

        /// <summary>
        /// As we get down on the tree the priority of operators increases.
        /// </summary>
        /// <param name="parentPriority"></param>
        /// <returns></returns>
        private Expresion ParseExpresionBinaria(int parentPrioridad = 0)
        {
            Expresion left;
            var operador_unario_prioridad = Current.tipo.get_prioridad_operador_unario();

            // it should be an plus minus token, and its priority should be the one of an unary operator.
            if (operador_unario_prioridad != 0 && operador_unario_prioridad >= parentPrioridad)
            {
                var operatorToken = NextToken();
                var operando = ParseExpresionPrimaria();
                left = new ExpresionUnaria(operando, operatorToken);
            }
            else
            {
                // there is no unary operator, normal primary expresion.
                left = ParseExpresionPrimaria();
            }
            while (true)
            {
                var prioridad = Current.tipo.get_prioridad_operador_binario();
                if (prioridad == 0 || prioridad <= parentPrioridad)
                {
                    break;
                }
                var operatorToken = NextToken();
                var right = ParseExpresionBinaria(prioridad);
                left = new ExpresionBinaria(left, operatorToken, right);
            }

            return left;
        }



        /*
        an primary expression is either a number or an parenthesized term.
        */
        private Expresion ParseExpresionPrimaria()
        {
            switch (Current.tipo)
            {
                case Tipo.OpenParenthesisToken:
                    return ParseParenthesizedExpression();
                case Tipo.TrueKeyword:
                case Tipo.FalseKeyword:
                    return ParseBooleanLiteral();
                case Tipo.NumberToken:
                    return ParseNumberLiteral();
                default: 
                    return ParseNameExpression();

            }

        }


        private Expresion ParseNumberLiteral()
        {
            var numberToken = Match(Tipo.NumberToken);
            return new ExpresionLiteral(numberToken);
        }

        private Expresion ParseParenthesizedExpression()
        {
            var left = Match(Tipo.OpenParenthesisToken);
            var expression = ParseAsignacion();
            var right = Match(Tipo.CloseParenthesisToken);
            return new ExpresionParéntesis(left, expression, right);
        }

        private Expresion ParseBooleanLiteral()
        {
            var is_True = Current.tipo == Tipo.TrueKeyword;
            var keywordToken = is_True ? Match(Tipo.TrueKeyword) : Match(Tipo.FalseKeyword);
            return new ExpresionLiteral(keywordToken, is_True);
        }

        private Expresion ParseNameExpression()
        {
            var identificador = Match(Tipo.IdentifierToken);
            return new ExpresionNombre(identificador);
        }
    }
}
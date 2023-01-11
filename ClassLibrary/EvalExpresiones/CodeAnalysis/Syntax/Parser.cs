namespace Eval;
internal sealed class Parser
{
    private readonly SyntaxToken[] _tokens; // an array with all the tokens generated by the lexer. not include spaces and bad tokens.
    private int _position; // in what position is the parsing. a pointer.
    private List<string> _diagnostics = new List<string>(); // every error should be stored here.
    public Parser(string text)
    {
        /*
        store the information in the parser properties.
        */
        var tokens = new List<SyntaxToken>();
        var lexer = new Lexer(text);
        SyntaxToken token;
        do
        {
            token = lexer.Lex();
            if
            (
                token.Kind != SyntaxKind.WhiteSpaceToken &&
                token.Kind != SyntaxKind.BadToken
            )
            {
                tokens.Add(token);
            }
        } while (token.Kind != SyntaxKind.EndOfFileToken);

        _diagnostics.AddRange(lexer.Diagnostics);
        _tokens = tokens.ToArray();
    }
    public IEnumerable<string> Diagnostics => _diagnostics;
    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;
        if (index >= _tokens.Length)
        {
            return _tokens[_tokens.Length - 1];
        }
        return _tokens[index];
    }
    private SyntaxToken Current => Peek(0);
    private SyntaxToken NextToken()
    {
        var current = Current;
        _position++;
        return current;
    }

    private SyntaxToken MatchToken(SyntaxKind kind)
    {
        if (Current.Kind == kind)
        {
            return NextToken();  // this returns the actual token, and move forward the pointer.
        }
        _diagnostics.Add($"ERROR: Unexpected token <{Current.Kind}> expected <{kind}> ");
        return new SyntaxToken(kind, Current.Position, null, null);
    }

    public SyntaxTree Parse()
    {
        var expression = ParseExpression();
        var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);
        // The tree is made up of ExpressionSyntax objects,
        return new SyntaxTree(_diagnostics, expression, endOfFileToken);
    }
    /// <summary>
    /// A recursive ParseExpression Method which work using Priorities. 
    /// </summary>
    /// <param name="parentPrecedence"> This represents the priority of the parent root.</param>
    /// <returns></returns>
    private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
    {
        // we are going to create a new node taking in-count the priority of the Parent, so 
        // that always parent priority is less than node priority. This implies that: as long
        // as the priority of the parent node is less than the child we are fine.

        // parse the PrimaryExpression at the Left.
        ExpressionSyntax left;
        var unaryOperatorPrecedence = Current.Kind.GetUnaryOperatorPrecedence();
        if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
        {
            var operatorToken = NextToken();
            var operand = ParseExpression(unaryOperatorPrecedence);
            left = new UnaryExpressionSyntax(operand, operatorToken);
        }
        else
        {
            left = ParsePrimaryExpression();
        }
        // now we expect more PrimaryExpression's separated by operators. and because first recursive
        // call is parentPrecedence = 0, this means that all operators will be handled in some moment.
        while (true)
        {
            var precedence = Current.Kind.GetBinaryOperatorPrecedence();
            // as long as the operators have more priority we keep building the tree.
            if (precedence == 0 || precedence <= parentPrecedence)
            {
                // stop condition to recursion we have either that there is no operator, so 
                // precedence es 0 or we find an operator that have less precedence than the parent root. 
                break;
            }
            var operatorToken = NextToken();
            var right = ParseExpression(precedence);
            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }
        return left;
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        switch (Current.Kind)
        {
            case SyntaxKind.OpenParenthesisToken:
                {
                    var left = NextToken();
                    var expression = ParseExpression();
                    var right = MatchToken(SyntaxKind.CloseParenthesisToken);
                    return new ParenthesizedExpressionSyntax(left, expression, right);
                }

            case SyntaxKind.TrueKeyword:
            case SyntaxKind.FalseKeyword:
                {
                    var keywordToken = NextToken();
                    var value = keywordToken.Kind == SyntaxKind.TrueKeyword;
                    return new LiteralExpressionSyntax(Current, value);
                }
            default:
                {
                    var numberToken = MatchToken(SyntaxKind.NumberToken);
                    return new LiteralExpressionSyntax(numberToken);
                }
        }

    }
}
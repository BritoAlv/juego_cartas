namespace Eval
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {
        public ParenthesizedExpressionSyntax(SyntaxToken openParenthezistoken, ExpressionSyntax expression, SyntaxToken closeParenthezis)
        {
            OpenParenthezistoken = openParenthezistoken;
            Expression = expression;
            CloseParenthezis = closeParenthezis;
        }

        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
        public SyntaxToken OpenParenthezistoken { get; }
        public ExpressionSyntax Expression { get; }
        public SyntaxToken CloseParenthezis { get; }
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthezistoken;
            yield return Expression;
            yield return CloseParenthezis;
        }
    }
}

namespace Eval
{
    public sealed class UnaryExpressionSyntax : ExpressionSyntax
    {
        public UnaryExpressionSyntax(ExpressionSyntax operand, SyntaxToken operatorToken)
        {
            Operand = operand;
            OperatorToken = operatorToken;
        }

        public override SyntaxKind Kind => SyntaxKind.UnaryExpression;
        public ExpressionSyntax Operand { get; }
        public SyntaxToken OperatorToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OperatorToken;
            yield return Operand;
        }
    }

}

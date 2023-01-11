namespace Eval
{
    internal static class SyntaxFacts
    {
        public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
        {
            /*
            operands with higher precedence should be executed first. priority
            */
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 3;
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 4;
                case SyntaxKind.AmpersandAmpersandToken:
                case SyntaxKind.Mayor:
                    return 2;

                 case SyntaxKind.PipePipeToken:
                    return 1;
                default:
                    return 0;
            }
        }

        public static int GetUnaryOperatorPrecedence(this SyntaxKind kind)
        {
            /*
            operands with higher precedence should be executed first. priority
            */
            switch (kind)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                case SyntaxKind.BangToken:
                    return 5;
                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }
}

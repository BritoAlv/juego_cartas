namespace Eval
{
    /*
    An enum with all the syntax kinds for our lexer. This is the structure of our language.
    */
    public enum SyntaxKind
    {
        // Tokens

        // special tokens
        EndOfFileToken,
        BadToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        NumberToken,
        UnaryExpression,
        LiteralExpression,
        BinaryExpression,
        ParenthesizedExpression,
        TrueKeyword,
        FalseKeyword,
        IdentifierToken,
        BangToken,
        AmpersandAmpersandToken,
        PipePipeToken,
        Mayor
    }
}

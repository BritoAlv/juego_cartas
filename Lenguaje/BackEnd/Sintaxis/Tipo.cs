namespace AnálisisCodigo.Sintaxis
{

    /// <summary>
    /// This enum contains all different kind of token that may exist in our language. 
    /// </summary>
    public enum Tipo
    {
        // Tokens:

        // Special Tokens
        EndOfFileToken,
        BadToken,
        WhiteSpaceToken,

        // Operators
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,

        BangToken,
        AmpersandToken,
        PipeToken,
        Equal,
        AsignacionToken,
        Distinto,

        // Syntax
        OpenParenthesisToken,
        CloseParenthesisToken,

        // Expressions
        ExpresionLiteral,
        ExpresionParéntesis,
        ExpresionBinaria,
        ExpresionAsignacion,
        ExpresionNombre,
        ExpresionUnaria,

        // Keywords
        TrueKeyword,
        FalseKeyword,
        NumberToken,
        IdentifierToken,
    }
}
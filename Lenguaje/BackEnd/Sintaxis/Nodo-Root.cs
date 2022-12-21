namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// Represent root of our tree.
    /// </summary>
    public sealed class NodoRoot
    {
        public NodoRoot(SourceText text, DiagnosticBag diagnostics, Expresion root, Token endOfFileToken)
        {
            Text = text;
            Diagnostics = diagnostics;
            Root = root;
            EndOfFileToken = endOfFileToken;
        }

        public SourceText Text { get; }
        public DiagnosticBag Diagnostics { get; }
        public Expresion Root { get; }
        public Token EndOfFileToken { get; }
        public static NodoRoot Parse(string text)
        {
            var sourceText = SourceText.From(text);
            return Parse(sourceText);
        }
        public static NodoRoot Parse(SourceText text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }

    }
}
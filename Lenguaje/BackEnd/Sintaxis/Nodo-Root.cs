namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// Represent root of our tree.
    /// </summary>
    public sealed class NodoRoot
    {
        public NodoRoot(DiagnosticBag diagnostics, Expresion root, Token endOfFileToken)
        {
            Diagnostics = diagnostics;
            Root = root;
            EndOfFileToken = endOfFileToken;
        }
        public DiagnosticBag Diagnostics { get; }
        public Expresion Root { get; }
        public Token EndOfFileToken { get; }
        public static NodoRoot Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }

        
    }
}
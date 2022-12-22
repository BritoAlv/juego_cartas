namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// Represent root of our tree.
    /// </summary>
    public sealed class NodoRoot
    {
        private NodoRoot(SourceText text)
        {
            var parser = new Parser(text);
            var root = parser.ParseCompilationUnit();
            var diagnostics = parser.Diagnostics;
            Text = text;
            Diagnostics = diagnostics;
            Root = root;
        }

        public SourceText Text { get; }
        public DiagnosticBag Diagnostics { get; }
        public CompilationunitSyntax Root { get; }
        public static NodoRoot Parse(string text)
        {
            var sourceText = SourceText.From(text);
            return Parse(sourceText);
        }
        public static NodoRoot Parse(SourceText text)
        {
            return new NodoRoot(text);
        }

    }
}
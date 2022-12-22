namespace AnálisisCodigo.Sintaxis
{
    public sealed class CompilationunitSyntax : Expresion
    {
        public CompilationunitSyntax(Expresion expresion, Token endOfFileToken)
        {
            Expresion = expresion;
            EndOfFileToken = endOfFileToken;
        }

        public override Tipo tipo => Tipo.CompilacionUnit;
        public Expresion Expresion { get; }
        public Token EndOfFileToken { get; }

        public override IEnumerable<Nodo> Hijos()
        {
            foreach (var hijo in Expresion.Hijos())
            {
                yield return hijo;
            }
        }
    }
}

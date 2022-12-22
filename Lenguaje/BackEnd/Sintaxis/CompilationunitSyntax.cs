namespace AnálisisCodigo.Sintaxis
{
    public sealed class CompilationunitSyntax : Expresion
    {
        public CompilationunitSyntax(Statement expresion, Token endOfFileToken)
        {
            Statement = expresion;
            EndOfFileToken = endOfFileToken;
        }

        public override Tipo tipo => Tipo.CompilacionUnit;
        public Statement Statement { get; }
        public Token EndOfFileToken { get; }

        public override IEnumerable<Nodo> Hijos()
        {
            foreach (var hijo in Statement.Hijos())
            {
                yield return hijo;
            }
        }
    }
}

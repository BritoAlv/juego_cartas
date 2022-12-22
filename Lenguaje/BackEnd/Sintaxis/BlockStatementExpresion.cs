namespace AnálisisCodigo.Sintaxis
{
    public sealed class BlockStatementExpresion : Statement
    {
        public BlockStatementExpresion(Nodo openBrace, List<Statement> statements, Nodo closedBrace)
        {
            OpenBrace = openBrace;
            Statements = statements;
            ClosedBrace = closedBrace;
        }
        public override Tipo tipo => Tipo.BlockStatement;
        public Nodo OpenBrace { get; }
        public List<Statement> Statements { get; }
        public Nodo ClosedBrace { get; }
        public override object value => throw new NotImplementedException();
        public override IEnumerable<Nodo> Hijos()
        {
            yield return OpenBrace;
            foreach (var statement in Statements)
            {
                yield return statement;
            }
            yield return ClosedBrace;
        }
    }
}

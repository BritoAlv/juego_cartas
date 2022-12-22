namespace AnálisisCodigo.Tipado
{
    internal sealed class BoundGlobalScope
    {
        public BoundGlobalScope(BoundGlobalScope previous, IEnumerable<Diagnostics> diagnostics, List<VariableSymbol> variables, StatementTipado statement)
        {
            Previous = previous;
            Diagnostics = diagnostics;
            Variables = variables;
            Statement = statement;
        }

        public BoundGlobalScope Previous { get; }
        public IEnumerable<Diagnostics> Diagnostics { get; }
        public List<VariableSymbol> Variables { get; }
        public StatementTipado Statement { get; }
    }



}
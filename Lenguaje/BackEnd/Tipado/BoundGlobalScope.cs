namespace AnálisisCodigo.Tipado
{
    internal sealed class BoundGlobalScope
    {
        public BoundGlobalScope(BoundGlobalScope previous, IEnumerable<Diagnostics> diagnostics, List<VariableSymbol> variables, ExpresionTipada expresion)
        {
            Previous = previous;
            Diagnostics = diagnostics;
            Variables = variables;
            Expresion = expresion;
        }

        public BoundGlobalScope Previous { get; }
        public IEnumerable<Diagnostics> Diagnostics { get; }
        public List<VariableSymbol> Variables { get; }
        public ExpresionTipada Expresion { get; }
    }



}
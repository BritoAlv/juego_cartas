using AnálisisCodigo.Sintaxis;
namespace AnálisisCodigo
{
    public sealed class Compilacion
    {
        public Compilacion(NodoRoot arbolsintax)
        {
            Arbolsintax = arbolsintax;
        }
        private NodoRoot Arbolsintax { get; }
        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
        {
            var tipador = new Tipado.Tipado(variables);
            var expresiontipada = tipador.Tipador(Arbolsintax.Root);

            var diagnostics = Arbolsintax.Diagnostics.Concat(tipador.Diagnostics);
            if (diagnostics.Any())
            {
                return new EvaluationResult(diagnostics, null);
            }
            var evaluator = new Evaluator(expresiontipada, variables);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<Diagnostics>(), value);
        }
    }
}
using AnálisisCodigo.Sintaxis;
using AnálisisCodigo.Tipado;
namespace AnálisisCodigo
{
    public sealed class Compilacion
    {
        private BoundGlobalScope _globalScope;
        public Compilacion(NodoRoot arbolsintax) : this(null, arbolsintax)
        {
            Arbolsintax = arbolsintax;
        }
        public Compilacion(Compilacion previous, NodoRoot arbolsintax)
        {
            Previous = previous;
            Arbolsintax = arbolsintax;
        }
        private NodoRoot Arbolsintax { get; }
        internal BoundGlobalScope GlobalScope
        {
            get
            {
                if(_globalScope == null)
                {
                    _globalScope = Tipado.Tipado.BindGlobalScope(Previous?.GlobalScope, Arbolsintax.Root);
                }
                return _globalScope;
            }
        }
        public Compilacion Previous { get; }

        public Compilacion ContinueWith(NodoRoot syntaxtree)
        {
            return new Compilacion(this, syntaxtree);
        }
        public EvaluationResult Evaluate(Dictionary<VariableSymbol, object> variables)
        {
            var diagnostics = Arbolsintax.Diagnostics.Concat(this.GlobalScope.Diagnostics);
            if (diagnostics.Any())
            {
                return new EvaluationResult(diagnostics, null);
            }
            var evaluator = new Evaluator(GlobalScope.Statement, variables);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<Diagnostics>(), value);
        }
    }
}
using AnálisisCodigo.Sintaxis;
namespace AnálisisCodigo.Tipado
{
    internal sealed class Tipado
    {
        private readonly DiagnosticBag _diagnostics = new DiagnosticBag();
        private BoundScope _scope;
        public Tipado(BoundScope parent)
        {
            _scope = new BoundScope(parent);
        }
        public static BoundGlobalScope BindGlobalScope(BoundGlobalScope previous, CompilationunitSyntax syntax)
        {
            var parentScope = CreateParentScope(previous);
            var binder = new Tipado(parentScope);
            var expression = binder.Tipador(syntax.Expresion);
            var variables = binder._scope.GetDeclaredVariables();
            var diagnostics = binder._diagnostics.ToList();

            if (previous != null)
            {
                diagnostics.InsertRange(0, previous.Diagnostics);
            }
            return new BoundGlobalScope(previous, diagnostics, variables, expression);
        }
        public DiagnosticBag Diagnostics => _diagnostics;
        public ExpresionTipada Tipador(Expresion A)
        {
            switch (A.tipo)
            {
                case Tipo.ExpresionLiteral:
                    return TiparExpresionLiteral((ExpresionLiteral)A);
                case Tipo.ExpresionUnaria:
                    return TiparExpresionUnaria((ExpresionUnaria)A);
                case Tipo.ExpresionBinaria:
                    return TiparExpresionBinaria((ExpresionBinaria)A);
                case Tipo.ExpresionParéntesis:
                    return Tipador(((ExpresionParéntesis)A).Expresion);
                case Tipo.ExpresionNombre:
                    return TiparExpresionNombre((ExpresionNombre)A);
                case Tipo.ExpresionAsignacion:
                    return TiparExpresionAsignacion((ExpresionAsignacion)A);
                default:
                    throw new Exception($"Unexpected Syntax {A.tipo}");
            }
        }
        private static BoundScope CreateParentScope(BoundGlobalScope previous)
        {
            var stack = new Stack<BoundGlobalScope>();
            while(previous != null)
            {
                stack.Push(previous);
                previous = previous.Previous;
            }

            BoundScope parent = null;
            while (stack.Count > 0)
            {
                previous = stack.Pop();
                var scope = new BoundScope(parent);
                foreach (var v in previous.Variables)
                {
                    scope.TryDeclare(v);
                }
                parent = scope;
            }
            return parent;
        }
        private ExpresionTipada TiparExpresionAsignacion(ExpresionAsignacion a)
        {
            var name = a.Identificador.Text;
            var expresiontipada = Tipador(a.Expresion);
            if (!_scope.TryLookUp(name, out var variable))
            {
                variable = new VariableSymbol(name, expresiontipada.Type);
                _scope.TryDeclare(variable);
            }
            if (expresiontipada.Type != variable.Type)
            {
                Diagnostics.ReportCannotConvert(a.Identificador.Span, expresiontipada.Type, variable.Type);
                return expresiontipada;
            }
            return new ExpresionAsignacionTipada(variable, expresiontipada);
        }
        private ExpresionTipada TiparExpresionNombre(ExpresionNombre a)
        {
            var name = a.Identificador.Text;
            if (!_scope.TryLookUp(name, out var variable))
            {
                _diagnostics.ReportUndefinedName(a.Identificador.Span, name);
                return new ExpresionLiteralTipada(0);
            }
            return new ExpresionVariableTipada(variable);
        }
        private ExpresionTipada TiparExpresionBinaria(ExpresionBinaria a)
        {
            var left = Tipador(a.Left);
            var right = Tipador(a.Right);
            var operador = OperadorBinarioTipado.Tipar(a.Operador.tipo, left.Type, right.Type);
            if (operador == null)
            {
                _diagnostics.ReportUndefinedBinaryOperator(a.Operador.Span, a.Operador.Text, left.Type, right.Type);
                return left;
            }
            return new ExpresionBinariaTipada(left, operador, right);
        }
        private ExpresionTipada TiparExpresionUnaria(ExpresionUnaria a)
        {
            var operand = Tipador(a.Operand);
            var operador = OperadorUnarioTipado.Tipar(a.Operador.tipo, operand.Type);
            if (operador == null)
            {
                _diagnostics.ReportUndefinedUnaryOperator(a.Operador.Span, a.Operador.Text, operand.Type);
                return operand;
            }
            return new ExpresionUnariaTipada(operador, operand);
        }
        private ExpresionTipada TiparExpresionLiteral(ExpresionLiteral a)
        {
            var value = a.Value ?? 0;
            return new ExpresionLiteralTipada(value);
        }
    }
}
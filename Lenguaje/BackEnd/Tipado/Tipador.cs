using AnálisisCodigo.Sintaxis;
namespace AnálisisCodigo.Tipado
{
    internal sealed class Tipado
    {
        private readonly DiagnosticBag _diagnostics = new DiagnosticBag();
        private readonly Dictionary<VariableSymbol, object> _variables;

        public Tipado(Dictionary<VariableSymbol, object> variables)
        {
            this._variables = variables;
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

        private ExpresionTipada TiparExpresionAsignacion(ExpresionAsignacion a)
        {
            var name = a.Identificador.Text;
            var expresiontipada = Tipador(a.Expresion);
            var existingVariable = _variables.Keys.FirstOrDefault(v => v.Name == name);
            if (existingVariable != null)
            {
                _variables.Remove(existingVariable);
            }
            var variable = new VariableSymbol(name, expresiontipada.Type);
            _variables[variable] = null;
            return new ExpresionAsignacionTipada(variable, expresiontipada);
        }

        private ExpresionTipada TiparExpresionNombre(ExpresionNombre a)
        {
            var name = a.Identificador.Text;
            var variable = _variables.Keys.FirstOrDefault(v => v.Name == name);
            if (variable == null)
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
using System.Collections;
using AnálisisCodigo.Sintaxis;
namespace AnálisisCodigo
{
    public sealed class DiagnosticBag : IEnumerable<Diagnostics>
    {

        private readonly List<Diagnostics> _diagnostics = new List<Diagnostics>();

        public IEnumerator<Diagnostics> GetEnumerator() => _diagnostics.GetEnumerator();

        internal void ReportBadCharacter(int position, char current)
        {
            var message = $"ERROR: bad character input: '{current}'";
            Report(new TextSpan(position, 1), message);
        }

        internal void ReportInvalidNumber(TextSpan textSpan, string text, Type type)
        {
            var message = $"The number {text} isn't valid {type}.";
            Report(textSpan, message);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void Report(TextSpan span, string message)
        {
            var diagnostics = new Diagnostics(span, message);
            _diagnostics.Add(diagnostics);
        }

        public void AddRange(DiagnosticBag diagnostics)
        {
            _diagnostics.AddRange(diagnostics._diagnostics);
        }

        internal void ReportUnexpectedToken(TextSpan span, Tipo tipo_actual, Tipo tipo_esperado)
        {
            // good parch
            string message;
            if (tipo_actual == Tipo.EndOfFileToken && tipo_esperado == Tipo.NumberToken)
            {
                message = $"Error {tipo_actual}, se esperaba LiteralToken";
            }
            else
            {
                message = $"Error {tipo_actual}, se esperaba {tipo_esperado}";
            }
            Report(span, message);
        }

        internal void ReportUndefinedUnaryOperator(TextSpan span, string operator_text, Type operand_type)
        {
            var message = $"Unary Operator is {operator_text} is not defined for type {operand_type}";
            Report(span, message);
        }

        internal void ReportUndefinedBinaryOperator(TextSpan operator_span, string operator_text, Type typeleft, Type typeright)
        {
           var message = $"Binary operator is '{operator_text}' is not defined for types {typeleft}, {typeright}";
            Report(operator_span, message);
        }

        internal void ReportUndefinedName(TextSpan span, string name)
        {
            var message = $"Variable {name} no existe";
            Report(span, message);
        }
    }
}
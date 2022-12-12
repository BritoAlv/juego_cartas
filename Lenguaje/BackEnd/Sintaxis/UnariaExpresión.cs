namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// This represents an unary expression.
    /// </summary>
    internal sealed class ExpresionUnaria : Expresion
    {

        public ExpresionUnaria(Expresion operand, Token operador)
        {
            this.Operand = operand;
            this.Operador = operador;
        }
        public override Tipo tipo => Tipo.ExpresionUnaria;
        public Expresion Operand { get; }
        public Token Operador { get; }

        /// <summary>
        /// This is a recursive stuff.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Nodo> Hijos()
        {
            yield return Operador;
            yield return Operand;
        }
        public override object value => $"Expresion Unaria";
    }
}

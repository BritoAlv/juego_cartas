namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// This represents an expression with parenthesis.
    /// </summary>
    internal sealed class ExpresionParéntesis : Expresion
    {
        public ExpresionParéntesis(Token openparéntesis, Expresion expresion, Token closeparéntesis)
        {
            Openparéntesis = openparéntesis;
            Expresion = expresion;
            Closeparéntesis = closeparéntesis;
        }

        public override Tipo tipo => Tipo.ExpresionParéntesis;

        public Token Openparéntesis { get; }
        public Expresion Expresion { get; }
        public Token Closeparéntesis { get; }

        public override IEnumerable<Nodo> Hijos()
        {
            yield return Openparéntesis;
            yield return Expresion;
            yield return Closeparéntesis;
        }

        public override object value => $"Expresion Paréntesis ";

    }
}
namespace AnálisisCodigo.Sintaxis
{
    /// <summary>
    /// This represents an literal expression
    /// </summary>
    internal sealed class ExpresionLiteral : Expresion
    {
        public ExpresionLiteral(Token literal) : this(literal, literal.Value)
        {
        }
        public ExpresionLiteral(Token literal, object value)
        {
            Literal = literal;
            Value = value;
        }
        public override Tipo tipo
        {
            get
            {
                return Tipo.ExpresionLiteral;
            }
        }
        public Token Literal { get; }
        public object Value { get; }
        public override IEnumerable<Nodo> Hijos()
        {
            yield return Literal;
        }
        public override object value => $"Expresion Literal ";
    }
}
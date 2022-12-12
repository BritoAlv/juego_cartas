namespace AnálisisCodigo.Tipado
{
    /// <summary>
    /// Represent the Literal Expression, Contains the value of the object.
    /// Because everything else is build on top of this.
    /// </summary>
    internal sealed class ExpresionLiteralTipada : ExpresionTipada
    {
        public ExpresionLiteralTipada(object value)
        {
            Value = value;
        }

        public override Type Type => Value.GetType();

        public override TipoNodoTipado tipo => TipoNodoTipado.ExpresionLiteral;

        public object Value { get; }
    }



}
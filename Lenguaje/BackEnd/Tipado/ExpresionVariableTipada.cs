namespace AnálisisCodigo.Tipado
{
    internal sealed class ExpresionVariableTipada : ExpresionTipada
    {
        public ExpresionVariableTipada(VariableSymbol variableSymbol)
        {
            VariableSymbol = variableSymbol;

        }
        public override Type Type => VariableSymbol.Type;

        public override TipoNodoTipado tipo => TipoNodoTipado.ExpresionVariable;

        public VariableSymbol VariableSymbol { get; }
    }



}
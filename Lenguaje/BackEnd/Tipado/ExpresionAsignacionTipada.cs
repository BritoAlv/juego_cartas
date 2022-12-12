using AnálisisCodigo.Sintaxis;

namespace AnálisisCodigo.Tipado
{
    internal class ExpresionAsignacionTipada : ExpresionTipada
    {
        public VariableSymbol VariableSymbol { get; }
        public ExpresionTipada expresiontipada { get; }

        public ExpresionAsignacionTipada(VariableSymbol variableSymbol, ExpresionTipada expresiontipada)
        {
            VariableSymbol = variableSymbol;
            this.expresiontipada = expresiontipada;
        }

        public override Type Type => expresiontipada.Type;

        public override TipoNodoTipado tipo => TipoNodoTipado.ExpresionAsignación;
    }
}
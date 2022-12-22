namespace AnálisisCodigo.Tipado
{
    internal class ExpresionStatementTipada : StatementTipado
    {
        public ExpresionStatementTipada(ExpresionTipada expresion)
        {
            Expresion = expresion;
        }

        public override TipoNodoTipado tipo => TipoNodoTipado.ExpresionStatement;
        public ExpresionTipada Expresion { get; }
    }
}
namespace AnálisisCodigo.Tipado
{
    internal sealed class ExpresionUnariaTipada : ExpresionTipada
    {
        public ExpresionUnariaTipada(OperadorUnarioTipado operadorUnarioTipo, ExpresionTipada operand)
        {
            OperadorUnarioTipo = operadorUnarioTipo;
            Operand = operand;
        }
        public override Type Type => OperadorUnarioTipo.Result_Type;
        public OperadorUnarioTipado OperadorUnarioTipo { get; }
        public ExpresionTipada Operand { get; }
        public override TipoNodoTipado tipo => TipoNodoTipado.ExpresionUnaria;
    }



}
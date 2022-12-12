namespace AnálisisCodigo.Tipado
{
    internal sealed class ExpresionBinariaTipada : ExpresionTipada
    {
        public ExpresionBinariaTipada(ExpresionTipada left, OperadorBinarioTipado operadorBinario, ExpresionTipada right)
        {
            Left = left;
            OperadorBinario = operadorBinario;
            Right = right;
        }
        /*
        The type of this binary operation is determined by the result of the operator.
        */
        public override Type Type => OperadorBinario.Result_Type;

        public override TipoNodoTipado tipo => TipoNodoTipado.ExpresionBinaria;

        public ExpresionTipada Left { get; }
        public OperadorBinarioTipado OperadorBinario { get; }
        public ExpresionTipada Right { get; }
    }



}
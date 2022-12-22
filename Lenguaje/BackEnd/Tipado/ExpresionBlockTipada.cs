namespace AnálisisCodigo.Tipado
{
    internal class ExpresionBlockTipada : StatementTipado
    {
        public ExpresionBlockTipada(List<StatementTipado> statements)
        {
            Statements = statements;
        }

        public override TipoNodoTipado tipo => TipoNodoTipado.BlockStatement;

        public List<StatementTipado> Statements { get; }
    }
}
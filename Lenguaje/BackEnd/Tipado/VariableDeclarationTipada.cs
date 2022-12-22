namespace AnálisisCodigo.Tipado
{
    internal sealed class VariableDeclarationTipada : StatementTipado
    {
        public VariableDeclarationTipada(VariableSymbol variable, ExpresionTipada initializer)
        {
            Variable = variable;
            Initializer = initializer;
        }
        public override TipoNodoTipado tipo => TipoNodoTipado.VariableDeclaration;
        public VariableSymbol Variable { get; }
        public ExpresionTipada Initializer { get; }
    }
}
namespace AnálisisCodigo.Sintaxis
{
    public sealed class ExpresionStatement : Statement
    {
        public ExpresionStatement(Expresion expresion)
        {
            Expresion = expresion;
        }
        public override Tipo tipo => Tipo.ExpresionStatement;
        public Expresion Expresion { get; }

        public override object value => Expresion.value;

        public override IEnumerable<Nodo> Hijos()
        {
            yield return Expresion;
        }
    }
}

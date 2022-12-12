namespace AnálisisCodigo.Sintaxis
{
    public sealed class ExpresionNombre : Expresion
    {
        public ExpresionNombre(Token identificador)
        {
            Identificador = identificador;
        }
        public override Tipo tipo => Tipo.ExpresionNombre;
        public Token Identificador { get; }

        public override IEnumerable<Nodo> Hijos()
        {
            yield return Identificador;
        }
        public override object value => $"Variable:  ";
    }
}

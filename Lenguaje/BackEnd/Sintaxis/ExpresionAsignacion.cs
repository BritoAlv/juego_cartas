namespace AnálisisCodigo.Sintaxis
{
    public sealed class ExpresionAsignacion : Expresion
    {
        public ExpresionAsignacion(Token identificador, Token equalsToken, Expresion expresion)
        {
            Identificador = identificador;
            EqualsToken = equalsToken;
            Expresion = expresion;
        }

        public Token Identificador { get; }
        public Token EqualsToken { get; }
        public Expresion Expresion { get; }

        public override Tipo tipo => Tipo.ExpresionAsignacion;

        public override IEnumerable<Nodo> Hijos()
        {
            yield return Identificador;
            yield return EqualsToken;
            yield return Expresion;
        }

        public override object value => $"Expresión Asignación =";
    }    
}

namespace AnálisisCodigo.Sintaxis
{
    public sealed class ExpresionAsignacion : Expresion
    {
        public ExpresionAsignacion(Token identificador, Token asignacionToken, Expresion expresion)
        {
            Identificador = identificador;
            AsignacionToken = asignacionToken;
            Expresion = expresion;
        }

        public Token Identificador { get; }
        public Token AsignacionToken { get; }
        public Expresion Expresion { get; }

        public override Tipo tipo => Tipo.ExpresionAsignacion;

        public override IEnumerable<Nodo> Hijos()
        {
            yield return Identificador;
            yield return AsignacionToken;
            yield return Expresion;
        }

        public override object value => $"Expresión Asignación =";
    }    
}

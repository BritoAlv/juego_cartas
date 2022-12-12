namespace AnálisisCodigo.Sintaxis
{

    /// <summary>
    /// This represents an binary expression.
    /// </summary>
    sealed class ExpresionBinaria : Expresion
    {
        public ExpresionBinaria(Expresion left, Token operador, Expresion right)
        {
            Left = left;
            Operador = operador;
            Right = right;
        }
        public override Tipo tipo => Tipo.ExpresionBinaria;
        public Expresion Left { get; }
        public Token Operador { get; }
        public Expresion Right { get; }

        /// <summary>
        /// This is a recursive stuff.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Nodo> Hijos()
        {
            yield return Left;
            yield return Operador;
            yield return Right;
        }
        public override object value => $"Expresion Binaria";
    }
}

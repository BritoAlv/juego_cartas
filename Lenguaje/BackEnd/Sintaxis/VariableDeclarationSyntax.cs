namespace AnálisisCodigo.Sintaxis
{
    public sealed class ExpresionVariableDeclaration : Statement
    {
        public ExpresionVariableDeclaration(Token keyword, Token identifier, Token equaltoken, Expresion initializer)
        {
            Keyword = keyword;
            Identifier = identifier;
            Equaltoken = equaltoken;
            Initializer = initializer;
        }

        public override Tipo tipo => Tipo.VariableDeclaration;
        public override object value => "Declaración Variable";
        public Token Keyword { get; }
        public Token Identifier { get; }
        public Token Equaltoken { get; }
        public Expresion Initializer { get; }

        public override IEnumerable<Nodo> Hijos()
        {
            yield return Keyword;
            yield return Identifier;
            yield return Equaltoken;
            yield return Initializer;
        }
    }
}

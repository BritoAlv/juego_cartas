namespace AnálisisCodigo.Sintaxis
{
    internal static class SyntaxFacts
    {
        public static int get_prioridad_operador_binario(this Tipo tipo)
        {
            switch (tipo)
            {
                case Tipo.PlusToken:
                case Tipo.MinusToken:
                    return 4;       

                case Tipo.AmpersandToken:
                    return 2;
                case Tipo.PipeToken:
                    return 1;

                case Tipo.StarToken:
                case Tipo.SlashToken:
                    return 5;

                case Tipo.Equal:
                case Tipo.Distinto:
                    return 3;

                default:
                    return 0;
            }
        }

        public static int get_prioridad_operador_unario(this Tipo tipo)
        {
            switch (tipo)
            {
                case Tipo.PlusToken:
                case Tipo.MinusToken:
                    return 6;
                case Tipo.BangToken:
                    return 6;
                default:
                    return 0;
            }
        }

        internal static Tipo GetKeyWordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return Tipo.TrueKeyword;
                case "false":
                    return Tipo.FalseKeyword;
                default:
                    return Tipo.IdentifierToken;
            }
        }
    }
}
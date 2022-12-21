namespace AnálisisCodigo.Sintaxis
{
    internal static class SyntaxFacts
    {
        public static string GetText(Tipo kind)
        {
            switch (kind)
            {
                case Tipo.PlusToken:
                    return "+";
                case Tipo.MinusToken:
                    return "-";
                case Tipo.StarToken:
                    return "*";
                case Tipo.SlashToken:
                    return "/";
                case Tipo.BangToken:
                    return "!";
                case Tipo.AsignacionToken:
                    return "=";
                case Tipo.Menor:
                    return "<";
                case Tipo.MenorIgual:
                    return "<=";
                case Tipo.Mayor:
                    return ">";
                case Tipo.MayorIgual:
                    return ">=";
                case Tipo.AmpersandToken:
                    return "&&";
                case Tipo.PipeToken:
                    return "||";
                case Tipo.Equal:
                    return "==";
                case Tipo.Distinto:
                    return "!=";
                case Tipo.OpenParenthesisToken:
                    return "(";
                case Tipo.CloseParenthesisToken:
                    return ")";
                case Tipo.OpenBraceToken:
                    return "{";
                case Tipo.CloseBraceToken:
                    return "}";
                case Tipo.Else:
                    return "else";
                case Tipo.FalseKeyword:
                    return "false";
                case Tipo.For:
                    return "for";
                case Tipo.If:
                    return "if";
                case Tipo.Let:
                    return "let";
                case Tipo.To:
                    return "to";
                case Tipo.TrueKeyword:
                    return "true";
                case Tipo.Var:
                    return "var";
                case Tipo.While:
                    return "while";
                default:
                    return null;
            }
        }
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

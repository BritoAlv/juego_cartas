using AnálisisCodigo.Sintaxis;
namespace AnálisisCodigo.Tipado
{
    internal sealed class OperadorBinarioTipado
    {
        private OperadorBinarioTipado(Tipo tipo, TipoOperadorBinario tipo_operador, Type type) :
        this(tipo, tipo_operador, type, type, type)
        { }

        private OperadorBinarioTipado(Tipo tipo, TipoOperadorBinario tipo_oerador, Type input, Type output) :
        this(tipo, tipo_oerador, input, input, output)
        { }

        private OperadorBinarioTipado(Tipo tipo, TipoOperadorBinario tipo_operador, Type left_type, Type right_type, Type result_type)
        {
            Tipo = tipo;
            Tipo_Operador = tipo_operador;
            Left_Type = left_type;
            Right_Type = right_type;
            Result_Type = result_type;
        }


        private static OperadorBinarioTipado[] _operators =
        {
            new OperadorBinarioTipado(Tipo.PlusToken, TipoOperadorBinario.AdicionNumeros , typeof(int)),
            new OperadorBinarioTipado(Tipo.PlusToken, TipoOperadorBinario.AdicionBooleanos , typeof(bool), typeof(bool)),
            new OperadorBinarioTipado(Tipo.MinusToken, TipoOperadorBinario.Substraccion , typeof(int)),
            new OperadorBinarioTipado(Tipo.StarToken, TipoOperadorBinario.MultiplicacionNumeros , typeof(int)),
            new OperadorBinarioTipado(Tipo.StarToken, TipoOperadorBinario.MultiplicacionBooleanos , typeof(bool), typeof(bool)),
            new OperadorBinarioTipado(Tipo.SlashToken, TipoOperadorBinario.Division , typeof(int)),
            new OperadorBinarioTipado(Tipo.Equal, TipoOperadorBinario.Igualdad, typeof(int), typeof(bool)),
            new OperadorBinarioTipado(Tipo.Equal, TipoOperadorBinario.Igualdad, typeof(bool), typeof(bool)),
            new OperadorBinarioTipado(Tipo.Distinto, TipoOperadorBinario.Diferente    ,typeof(int), typeof(bool)),
            new OperadorBinarioTipado(Tipo.Distinto, TipoOperadorBinario.Diferente, typeof(bool), typeof(bool)),
            new OperadorBinarioTipado(Tipo.AmpersandToken, TipoOperadorBinario.LogicalAnd , typeof(bool)),
            new OperadorBinarioTipado(Tipo.PipeToken, TipoOperadorBinario.LogicalOr , typeof(bool)),
        };

        public Tipo Tipo { get; }
        public TipoOperadorBinario Tipo_Operador { get; }
        public Type Left_Type { get; }
        public Type Right_Type { get; }
        public Type Result_Type { get; }
        internal static OperadorBinarioTipado[] Operators { get => Operators1; set => Operators1 = value; }
        internal static OperadorBinarioTipado[] Operators1 { get => _operators; set => _operators = value; }

        public static OperadorBinarioTipado Tipar(Tipo kind, Type left_type, Type right_type)
        {
            foreach (var op in Operators)
            {
                if (kind == op.Tipo && op.Left_Type == left_type && op.Right_Type == right_type)
                {
                    return op;
                }
            }
            return null;
        }
    }



}
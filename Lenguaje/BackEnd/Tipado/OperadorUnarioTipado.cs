using AnálisisCodigo.Sintaxis;
namespace AnálisisCodigo.Tipado
{
    internal sealed class OperadorUnarioTipado
    {
        private OperadorUnarioTipado(Tipo tipo, TipoOperadorUnario tipo_operador, Type operand_type) :
        this(tipo, tipo_operador, operand_type, operand_type)
        {
        }

        private OperadorUnarioTipado(Tipo tipo, TipoOperadorUnario tipo_operador, Type operand_type, Type result_type)
        {
            Tipo = tipo;
            Tipo_Operador = tipo_operador;
            Operand_Type = operand_type;
            Result_Type = result_type;
        }

        public Tipo Tipo { get; }
        public TipoOperadorUnario Tipo_Operador { get; }
        public Type Operand_Type { get; }
        public Type Result_Type { get; }

        private static OperadorUnarioTipado[] _operators =
        {
            new OperadorUnarioTipado(Tipo.BangToken, TipoOperadorUnario.NegacionLogica , typeof(bool)),
            new OperadorUnarioTipado(Tipo.PlusToken, TipoOperadorUnario.Identidad , typeof(int)),
            new OperadorUnarioTipado(Tipo.MinusToken, TipoOperadorUnario.Negacion , typeof(int)),
        };

        public static OperadorUnarioTipado Tipar(Tipo kind, Type operand_type)
        {
            foreach (var op in _operators)
            {
                if (kind == op.Tipo && op.Operand_Type == operand_type)
                {
                    return op;
                }
            }
            return null;
        }
    }



}
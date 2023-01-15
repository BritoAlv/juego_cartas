namespace Poker
{
    internal class BinaryAction : Return<bool>
    {
        private Return<bool> condition1;
        private Token op;
        private Return<bool> condition2;
        public BinaryAction(Return<bool> condition1, Token op, Return<bool> condition2) : base(condition1.Open_Parenthesis, condition1.Signature, condition2.Closed_Parenthesis)
        {
            this.condition1 = condition1;
            this.op = op;
            this.condition2 = condition2;
        }

        public override IEnumerable<bool> Evaluate(IGlobal_Contexto contexto)
        {
            IEnumerable<bool> result1 = this.condition1.Evaluate(contexto);
            if (op.Text == "&&")
            {
                if (result1.All(x => x == true))
                {
                    IEnumerable<bool> result2 = this.condition2.Evaluate(contexto);
                    if (result2.All(x => x == true))
                    {
                        return new List<bool> { true };
                    }
                }
                return new List<bool> { false };
            }
            if (op.Text == "||")
            {
                if (result1.All(x => x == true))
                {
                    return new List<bool> { true };
                }
                IEnumerable<bool> result2 = this.condition2.Evaluate(contexto);
                if (result2.All(x => x == true))
                {
                    return new List<bool> {true};
                }
                return new List<bool> { false };
            }
            return new List<bool> { false };
        }

        public override bool Evaluate_Top(IGlobal_Contexto contexto)
        {
            return Evaluate(contexto).First();
        }

        public override IEnumerable<Iprintable> GetChildrenIprintables()
        {
            yield return condition1;
            yield return op;
            yield return condition2;
        }
    }
}
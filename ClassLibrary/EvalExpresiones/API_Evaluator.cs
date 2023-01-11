namespace Eval
{
    public class Evaluador
    {
        public static object Evaluator(string line)
        {
            var syntaxtree = SyntaxTree.Parse(line);
            var binder = new Binder();
            var BoundExpression = binder.BindExpression(syntaxtree.Root);
            var e = new Evaluator(BoundExpression);
            var result = e.Evaluate();
            return result;
        }
    }
}

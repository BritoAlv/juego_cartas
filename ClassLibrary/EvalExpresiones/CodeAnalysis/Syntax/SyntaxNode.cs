namespace Eval;
public abstract class SyntaxNode
{
    /*
    The node of our tree are of some Kind, and implement a way to give you in an enumerable of its child.
    */
    public abstract SyntaxKind Kind { get; }

    public abstract IEnumerable<SyntaxNode> GetChildren();
}

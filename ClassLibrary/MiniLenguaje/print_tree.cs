/// <summary>
/// You should implement this interface in your BinaryTree class:
/// </summary>
public interface Iprintable
{
    /// <summary>
    /// Given a node of the tree return in an IEnumerable its children.
    /// </summary> 
    IEnumerable<Iprintable> GetChildrenIprintables();

    /// <summary>
    /// what will be printed for that node.
    /// </summary>
    string valor { get; }
}

public static class print_tree
{
    /// <summary>
    /// Recursive function to print a tree in Unix style.
    /// </summary>
    /// <param name="node"> actual node we are </param>
    /// <param name="indent"> this is the indent used, its value depend on the depth of the node </param>
    /// <param name="isLast"> this refers : if the node is the last children when called the method of the interface </param>
    public static void print(Iprintable node, string indent = "", bool isLast = true)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        // determine which shape use in dependence of it is the last or there are more children in its level.
        var marker = isLast ? "└──" : "├──";

        // print the corresponding indent for that level of depth in the tree.
        Console.Write(indent);

        // print the marker.
        Console.Write(marker);

        // print what represents that node.+
        if (node is null)
        {
            return;
        }
        Console.Write(node.valor);


        // pass to the children nodes recursively.
        Console.WriteLine();

        // compute indent for children.
        indent += isLast ? "    " : "│   ";

        // call the method recursively.
        var childrens = node.GetChildrenIprintables();
        var lastChild = childrens.LastOrDefault();
        foreach (var child in childrens)
        {
            print(child, indent, child == lastChild);
        }
        Console.ResetColor();

    }
}
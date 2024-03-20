namespace Artin.Collections.Graph;

public class Graph<TNode> where TNode : INode
{
    public HashSet<TNode> Nodes { get; }

    public Graph(HashSet<TNode>? nodes = null)
    {
        Nodes = nodes ?? new HashSet<TNode>();
    }

    public Graph<TNode> AddNode(TNode node)
    {
        Nodes.Add(node);

        return this;
    }
}
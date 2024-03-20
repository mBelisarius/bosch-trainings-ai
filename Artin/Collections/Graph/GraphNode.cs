namespace Artin.Collections.Graph;

public class GraphNode<TValueNode, TValueEdge> : INode
{
    private TValueNode? _data;
    private List<IEdge> _neighbors;

    public object? Data
    {
        get => _data;
        set => _data = (TValueNode?)value;
    }

    public IReadOnlyCollection<IEdge> Neighbors => _neighbors;

    public int CountNeighbours => _neighbors.Count;
    
    public bool Equals(INode? other) => ReferenceEquals(other, this);
    
    public override bool Equals(object? obj) => Equals(obj as GraphNode<TValueNode, TValueEdge>);
    public override int GetHashCode() => (_data, _neighbors).GetHashCode();

    public static bool operator ==(GraphNode<TValueNode, TValueEdge> a, GraphNode<TValueNode, TValueEdge> b)
        => a.Equals(b);

    public static bool operator !=(GraphNode<TValueNode, TValueEdge> a, GraphNode<TValueNode, TValueEdge> b)
        => !(a == b);

    public GraphNode(
        TValueNode data,
        List<IEdge>? neighbors = null
    )
    {
        Data = data;
        _neighbors = neighbors ?? new List<IEdge>();
    }

    public GraphNode<TValueNode, TValueEdge> AddNode(IEdge edge)
    {
        if (!_neighbors.Contains(edge))
            _neighbors.Add((GraphEdge<TValueNode, TValueEdge>)edge);

        return this;
    }

    public GraphNode<TValueNode, TValueEdge> AddNode(INode node, TValueEdge weight)
    {
        var edge = new GraphEdge<TValueNode, TValueEdge>(weight, this, node);

        if (!_neighbors.Contains(edge))
            _neighbors.Add(edge);

        return this;
    }

    public GraphNode<TValueNode, TValueEdge> RemoveNode(IEdge edge)
    {
        _neighbors.Remove((GraphEdge<TValueNode, TValueEdge>)edge);

        return this;
    }

    public GraphNode<TValueNode, TValueEdge> RemoveNode(INode node)
    {
        _neighbors.RemoveAll(edge => edge.TargetNode.Equals(node));

        return this;
    }

    public void RemoveNodeAt(int index)
    {
        _neighbors.RemoveAt(index);
    }
}
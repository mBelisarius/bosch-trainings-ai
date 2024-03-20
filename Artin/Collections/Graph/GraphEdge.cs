namespace Artin.Collections.Graph;

public class GraphEdge<TValueNode, TValueEdge> : IEdge
{
    private TValueEdge?                       _data;
    private GraphNode<TValueNode, TValueEdge> _sourceNode = null!;
    private GraphNode<TValueNode, TValueEdge> _targetNode = null!;

    public object? Data
    {
        get => _data;
        set => _data = (TValueEdge?)value;
    }

    public INode SourceNode
    {
        get => _sourceNode;
        set => _sourceNode = (GraphNode<TValueNode, TValueEdge>)value;
    }

    public INode TargetNode
    {
        get => _targetNode;
        set => _targetNode = (GraphNode<TValueNode, TValueEdge>)value;
    }

    public GraphEdge(
        TValueEdge data,
        INode source,
        INode target
    )
    {
        Data = data;
        SourceNode = source;
        TargetNode = target;
    }
}
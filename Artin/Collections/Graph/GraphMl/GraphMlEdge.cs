namespace Artin.Collections.Graph.GraphMl;

public class GraphMlEdge : IEdge
{
    private          Dictionary<string, string>? _data;
    private readonly GraphMlParser               _graphMlParser;
    public           string                      SourceId { get; }
    public           string                      TargetId { get; }

    public object? Data
    {
        get => _data;
        set => _data = (Dictionary<string, string>?)value;
    }

    public INode SourceNode
    {
        get => _graphMlParser.Node(SourceId);
        set => throw new NotImplementedException();
    }

    public INode TargetNode
    {
        get => _graphMlParser.Node(TargetId);
        set => throw new NotImplementedException();
    }

    public GraphMlEdge(
        Dictionary<string, string> data = null!,
        string sourceId = null!,
        string targetId = null!,
        GraphMlParser graphMlParser = null!
    )
    {
        Data = data;
        SourceId = sourceId;
        TargetId = targetId;
        _graphMlParser = graphMlParser;
    }
}
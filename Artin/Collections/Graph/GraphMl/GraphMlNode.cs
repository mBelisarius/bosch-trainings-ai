namespace Artin.Collections.Graph.GraphMl;

public class GraphMlNode : INode
{
    private          Dictionary<string, string>? _data;
    private readonly GraphMlParser               _graphMlParser;
    private          int?                        EdgesHash      { get; set; }
    private          IReadOnlyCollection<IEdge>  NeighborsCache { get; set; } = null!;
    public           string                      Id             { get; }

    public object? Data
    {
        get => _data;
        set => _data = (Dictionary<string, string>?)value;
    }

    public IReadOnlyCollection<IEdge> Neighbors
    {
        get
        {
            if (EdgesHash is null || EdgesHash != _graphMlParser.EdgesValidateCache)
            {
                EdgesHash = _graphMlParser.EdgesValidateCache;
                NeighborsCache = _graphMlParser.Neighbors(Id);
            }

            return NeighborsCache;
        }
    }

    public int CountNeighbours => Neighbors.Count;

    public bool Equals(INode? other)
    {
        if (other is not GraphMlNode node)
            return false;

        return node.Id == Id;
    }

    public override bool Equals(object? obj) => Equals(obj as GraphMlNode);
    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(GraphMlNode a, GraphMlNode b)
        => a.Equals(b);

    public static bool operator !=(GraphMlNode a, GraphMlNode b)
        => !(a == b);

    public GraphMlNode(
        string id,
        Dictionary<string, string> data,
        GraphMlParser graphMlParser
    )
    {
        Id = id;
        Data = data;
        _graphMlParser = graphMlParser;
    }
}
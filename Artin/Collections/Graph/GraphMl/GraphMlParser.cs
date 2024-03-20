using System.Collections.Immutable;
using System.Xml;

namespace Artin.Collections.Graph.GraphMl;

public class GraphMlParser
{
    private XmlDocument                     XmlDoc { get; }
    private Dictionary<string, GraphMlNode> _nodes = null!;
    private MultiMap<string, GraphMlEdge>   _edges = null!;

    public ImmutableHashSet<GraphMlNode> NodesSet { get; private set; } = null!;
    public ImmutableHashSet<GraphMlEdge> EdgesSet { get; private set; } = null!;

    public int EdgesValidateCache { get; private set; }

    public GraphMlNode Node(string id) => _nodes[id];

    public IReadOnlyList<GraphMlEdge> Neighbors(string sourceId)
        => _edges[sourceId];

    public GraphMlParser(string path)
    {
        XmlDoc = new XmlDocument();
        XmlDoc.Load(path);
    }

    public ImmutableHashSet<GraphMlNode> ParseNodes()
    {
        var nodes = XmlDoc.GetElementsByTagName("node");
        var graphNodes = new Dictionary<string, GraphMlNode>(nodes.Count);

        foreach (XmlNode node in nodes)
        {
            var id = node.Attributes![0].Value;

            var data = new Dictionary<string, string>();
            foreach (XmlNode child in node.ChildNodes)
            {
                var dataKey = child.Attributes![0].Value;
                data[dataKey] = child.InnerText;
            }

            graphNodes.Add(id, new GraphMlNode(id, data, this));
        }

        _nodes = graphNodes;
        NodesSet = _nodes.Values.ToImmutableHashSet();

        return NodesSet;
    }

    public ImmutableHashSet<GraphMlEdge> ParseEdges()
    {
        var edges = XmlDoc.GetElementsByTagName("edge");
        var graphEdges = new MultiMap<string, GraphMlEdge>();

        foreach (XmlNode edge in edges)
        {
            string sourceId = null!;
            string targetId = null!;

            foreach (XmlAttribute attr in edge.Attributes!)
            {
                switch (attr.Name)
                {
                    case "source":
                        sourceId = attr.Value;
                        break;
                    case "target":
                        targetId = attr.Value;
                        break;
                }
            }

            var data = new Dictionary<string, string>();
            foreach (XmlNode child in edge.ChildNodes)
            {
                var dataKey = child.Attributes![0].Value;
                data[dataKey] = child.InnerText;
            }

            graphEdges.Add(sourceId, new GraphMlEdge(data, sourceId, targetId, this));
        }

        _edges = graphEdges;
        EdgesValidateCache++;
        EdgesSet = _edges.Values.ToImmutableHashSet();

        return EdgesSet;
    }
}
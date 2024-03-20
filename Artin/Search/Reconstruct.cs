using Artin.Collections;

namespace Artin.Search;

public static partial class Search
{
    private static List<INode> ReconstructPath(INode start, INode goal, IDictionary<INode, INode> pathVisited)
    {
        var path = new List<INode>();
        
        var node = goal;
        while (!Equals(node, start))
        {
            path.Add(node);
            node = pathVisited[node];
        }
        path.Add(start);
        path.Reverse();
        
        return path;
    }
}
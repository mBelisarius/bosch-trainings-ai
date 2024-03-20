using Artin.Collections;

namespace Artin.Search;

public static partial class Search
{
    public static List<INode>? Bfs(INode start, INode goal)
    {
        var queue = new Queue<INode>();
        var visited = new HashSet<INode>();
        var path = new Dictionary<INode, INode>();
        
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();

            if (!visited.Add(currNode))
                continue;

            foreach (IEdge neighbor in currNode.Neighbors)
            {
                var targetNode = neighbor.TargetNode;
                if (visited.Contains(targetNode))
                    continue;
                
                path.TryAdd(targetNode, currNode);
                queue.Enqueue(targetNode);
                
                if (targetNode.Equals(goal))
                    return ReconstructPath(start, targetNode, path);
            }
        }

        return null;
    }
}
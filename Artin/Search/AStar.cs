using Artin.Collections;

namespace Artin.Search;

public static partial class Search
{
    public static List<INode>? AStar(
        INode start,
        INode goal,
        Func<IEdge, float> weights,
        Func<INode, INode, float> penalty
    )
    {
        var queue = new PriorityQueue<INode, float>();
        var visited = new HashSet<INode>();
        var dist = new Dictionary<INode, float>();
        var path = new Dictionary<INode, INode>();

        queue.Enqueue(start, 0.0f);
        dist[start] = 0.0f;
        dist[goal] = float.PositiveInfinity;

        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();

            if (!visited.Add(currNode))
                continue;

            foreach (IEdge neighbor in currNode.Neighbors)
            {
                var targetNode = neighbor.TargetNode;

                var newDist = dist[currNode] + weights(neighbor);

                if (newDist >= dist[goal] || newDist >= dist.GetValueOrDefault(targetNode, float.PositiveInfinity))
                    continue;

                dist[targetNode] = newDist;
                path[targetNode] = currNode;
                queue.Enqueue(targetNode, newDist + penalty(targetNode, goal));

                if (targetNode.Equals(goal)) 
                    return ReconstructPath(start, targetNode, path);
            }
        }

        return null;
    }
}
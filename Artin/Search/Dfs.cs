using Artin.Collections;

namespace Artin.Search;

public static partial class Search
{
    public static List<INode>? Dfs(INode start, INode goal)
    {
        var stack = new Stack<INode>();
        var visited = new HashSet<INode>();
        var path = new Dictionary<INode, INode>();
        
        stack.Push(start);

        while (stack.Count > 0)
        {
            var currNode = stack.Pop();

            if (!visited.Add(currNode))
                continue;
            
            foreach (IEdge neighbor in currNode.Neighbors)
            {
                var targetNode = neighbor.TargetNode;
                if (visited.Contains(targetNode))
                    continue;
                
                path.TryAdd(targetNode, currNode);
                stack.Push(targetNode);

                if (targetNode.Equals(goal))
                    return ReconstructPath(start, targetNode, path);
            }
        }

        return null;
    }
}
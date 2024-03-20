using System.Collections;
using System.Diagnostics;
using System.Globalization;
using Artin.Collections;
using Artin.Collections.Graph.GraphMl;
using Artin.Search;

public static class ExeRoute
{
    public static void Execute()
    {
        var graph = new GraphMlParser("curitiba.graphml");
        graph.ParseNodes();
        graph.ParseEdges();

        var startNode = graph.NodesSet.FirstOrDefault(node => node.Id == "8755170456")!;
        var endNode = graph.NodesSet.FirstOrDefault(node => node.Id == "9938088199")!;

        Console.WriteLine($"{startNode.Id} -> {endNode.Id}");
        PrintAStar(startNode, endNode, 10);
        // PrintDijkstra(startNode, endNode, 1);
        // PrintBsf(startNode, endNode, 1);
        // PrintDsf(startNode, endNode);
    }

    private static void PrintDsf(INode start, INode goal, int repeat = 1)
    {
        var sw = new Stopwatch();
        sw.Start();

        List<INode?> path = null!;
        for (int i = 0; i < repeat; i++)
            path = Search.Dfs(start, goal);

        sw.Stop();
        Console.Write($"{sw.ElapsedMilliseconds} | ");

        if (path is null)
            Console.WriteLine("Goal not found");
        else
            foreach (var node in path.Cast<GraphMlNode>())
                Console.Write($"{node?.Id} ");

        Console.WriteLine();
    }

    private static void PrintBsf(INode start, INode goal, int repeat = 1)
    {
        var sw = new Stopwatch();
        sw.Start();

        List<INode?> path = null!;
        for (int i = 0; i < repeat; i++)
            path = Search.Bfs(start, goal);

        sw.Stop();
        Console.Write($"{sw.ElapsedMilliseconds} | ");

        if (path is null)
            Console.WriteLine("Goal not found");
        else
            foreach (var node in path.Cast<GraphMlNode>())
                Console.Write($"{node.Id} ");

        Console.WriteLine();
    }

    private static void PrintDijkstra(INode start, INode goal, int repeat = 1)
    {
        var sw = new Stopwatch();
        sw.Start();

        List<INode?> path = null!;
        for (int i = 0; i < repeat; i++)
            path = Search.Dijkstra(
                start,
                goal,
                edge =>
                {
                    var value = ((IDictionary)edge.Data)["d14"];
                    return float.Parse(value.ToString(), CultureInfo.InvariantCulture);
                });

        sw.Stop();
        Console.Write($"{sw.ElapsedMilliseconds} | ");

        if (path is null)
            Console.WriteLine("Goal not found");
        else
            foreach (var node in path.Cast<GraphMlNode>())
                Console.Write($"{node.Id} ");

        Console.WriteLine();
    }

    private static void PrintAStar(INode start, INode goal, int repeat = 1)
    {
        var sw = new Stopwatch();
        sw.Start();

        List<INode?> path = null!;
        for (int i = 0; i < repeat; i++)
        {
            path = Search.AStar(
                start,
                goal,
                edge =>
                {
                    var value = ((IDictionary)edge.Data)["d14"];
                    return float.Parse(value.ToString(), CultureInfo.InvariantCulture);
                },
                (curr, goal) =>
                {
                    var xCurr = float.Parse(((IDictionary)curr.Data)["d5"].ToString(),
                                            CultureInfo.InvariantCulture);
                    var xGoal = float.Parse(((IDictionary)goal.Data)["d5"].ToString(),
                                            CultureInfo.InvariantCulture);

                    var yCurr = float.Parse(((IDictionary)curr.Data)["d4"].ToString(),
                                            CultureInfo.InvariantCulture);
                    var yGoal = float.Parse(((IDictionary)goal.Data)["d4"].ToString(),
                                            CultureInfo.InvariantCulture);

                    var dx = xCurr - xGoal;
                    var dy = yCurr - yGoal;

                    return MathF.Sqrt(dx * dx + dy * dy);
                });
        }

        sw.Stop();
        Console.Write($"{sw.ElapsedMilliseconds} | ");

        if (path is null)
            Console.WriteLine("Goal not found");
        else
            foreach (var node in path.Cast<GraphMlNode>())
                Console.Write($"{node.Id} ");

        Console.WriteLine();
    }
}
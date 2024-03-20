using Artin.Collections.Graph;
using Artin.Search;

public static class ExeGraph
{
    public static void Execute()
    {
        var graph = BuildGraph();

        var startNode = graph.Nodes.FirstOrDefault(node => ReferenceEquals(node.Data, "New York"))!;
        var endNode = graph.Nodes.FirstOrDefault(node => ReferenceEquals(node.Data, "Chicago"))!;

        Console.WriteLine(startNode.Data);
        Console.WriteLine(endNode.Data);

        var pathDsf = Search.Dfs(startNode, endNode);
        if (pathDsf is null) Console.WriteLine("Goal not found");
        else
            foreach (var node in pathDsf)
                Console.Write($"{((GraphNode<string, float>)node).Data} ");
        Console.WriteLine();

        var pathBsf = Search.Bfs(startNode, endNode);
        if (pathBsf is null) Console.WriteLine("Goal not found");
        else
            foreach (var node in pathBsf)
                Console.Write($"{((GraphNode<string, float>)node).Data} ");
        Console.WriteLine();

        var pathDijkstra = Search.Dijkstra(startNode, endNode, edge => (float)edge.Data!);
        if (pathDijkstra is null) Console.WriteLine("Goal not found");
        else
            foreach (var node in pathDijkstra)
                Console.Write($"{((GraphNode<string, float>)node).Data} ");
        Console.WriteLine();
    }

    private static Graph<GraphNode<string, float>> BuildGraph()
    {
        var nodeBaltimore = new GraphNode<string, float>("Baltimore");
        var nodeBoston = new GraphNode<string, float>("Boston");
        var nodeBuffalo = new GraphNode<string, float>("Buffalo");
        var nodeChicago = new GraphNode<string, float>("Chicago");
        var nodeCleveland = new GraphNode<string, float>("Cleveland");
        var nodeColumbus = new GraphNode<string, float>("Columbus");
        var nodeDetroit = new GraphNode<string, float>("Detroit");
        var nodeIndianapolis = new GraphNode<string, float>("Indianapolis");
        var nodeNewYork = new GraphNode<string, float>("New York");
        var nodePhiladelphia = new GraphNode<string, float>("Philadelphia");
        var nodePittsburgh = new GraphNode<string, float>("Pittsburgh");
        var nodePortland = new GraphNode<string, float>("Portland");
        var nodeProvidence = new GraphNode<string, float>("Providence");
        var nodeSyracuse = new GraphNode<string, float>("Syracuse");

        nodeBaltimore.AddNode(nodePhiladelphia, 101)
                     .AddNode(nodePittsburgh, 247);

        nodeBoston.AddNode(nodeNewYork, 215)
                  .AddNode(nodePortland, 107)
                  .AddNode(nodeProvidence, 50)
                  .AddNode(nodeSyracuse, 312);

        nodeBuffalo.AddNode(nodeCleveland, 189)
                   .AddNode(nodeDetroit, 256)
                   .AddNode(nodePittsburgh, 215)
                   .AddNode(nodeSyracuse, 150);

        nodeChicago.AddNode(nodeCleveland, 345)
                   .AddNode(nodeDetroit, 283)
                   .AddNode(nodeIndianapolis, 182);

        nodeCleveland.AddNode(nodeBuffalo, 189)
                     .AddNode(nodeChicago, 345)
                     .AddNode(nodeColumbus, 144)
                     .AddNode(nodeDetroit, 169)
                     .AddNode(nodePittsburgh, 134);

        nodeColumbus.AddNode(nodeCleveland, 144)
                    .AddNode(nodeIndianapolis, 176)
                    .AddNode(nodePittsburgh, 185);

        nodeDetroit.AddNode(nodeBuffalo, 256)
                   .AddNode(nodeChicago, 283)
                   .AddNode(nodeCleveland, 169);

        nodeIndianapolis.AddNode(nodeChicago, 182)
                        .AddNode(nodeColumbus, 176);

        nodeNewYork.AddNode(nodeBoston, 215)
                   .AddNode(nodePhiladelphia, 97)
                   .AddNode(nodeProvidence, 181)
                   .AddNode(nodeSyracuse, 254);

        nodePhiladelphia.AddNode(nodeBaltimore, 101)
                        .AddNode(nodeNewYork, 97)
                        .AddNode(nodePittsburgh, 305)
                        .AddNode(nodeSyracuse, 253);

        nodePittsburgh.AddNode(nodeBaltimore, 247)
                      .AddNode(nodeBuffalo, 215)
                      .AddNode(nodeCleveland, 134)
                      .AddNode(nodeColumbus, 185)
                      .AddNode(nodePhiladelphia, 305);

        nodePortland.AddNode(nodeBoston, 107);

        nodeProvidence.AddNode(nodeBoston, 50)
                      .AddNode(nodeNewYork, 181);

        nodeSyracuse.AddNode(nodeBoston, 312)
                    .AddNode(nodeBuffalo, 150)
                    .AddNode(nodeNewYork, 254)
                    .AddNode(nodePhiladelphia, 253);

        var buildWeightedGraph = new Graph<GraphNode<string, float>>(
            new HashSet<GraphNode<string, float>>()
            {
                nodeBaltimore,
                nodeBoston,
                nodeBuffalo,
                nodeChicago,
                nodeCleveland,
                nodeColumbus,
                nodeDetroit,
                nodeIndianapolis,
                nodeNewYork,
                nodePhiladelphia,
                nodePittsburgh,
                nodePortland,
                nodeProvidence,
                nodeSyracuse,
            });

        return buildWeightedGraph;
    }
}
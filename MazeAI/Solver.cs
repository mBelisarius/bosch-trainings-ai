namespace MazeAI;

public class Solver
{
    public float Distance { get; set; }
    public int   Option   { get; set; }
    public Maze  Maze     { get; set; } = null!;

    public string Algorithm
    {
        get
        {
            return (Option % 4) switch
            {
                0 => "DFS",
                1 => "BFS",
                2 => "dijkstra",
                _ => "aStar"
            };
        }
    }

    public void Solve()
    {
        Distance = 0.0f;
        
        var goal = Maze.Spaces.FirstOrDefault(s => s.Exit);

        if (Maze.Root is null || goal is null)
            return;

        switch (Option % 4)
        {
            case 0:
                Dfs(Maze.Root, goal);
                break;
            case 1:
                Bfs(Maze.Root, goal);
                break;
            case 2:
                Dijkstra(Maze.Root, goal);
                break;
            case 3:
                AStar(Maze.Root, goal);
                break;
        }
    }

    private bool Dfs(Space space, Space goal)
    {
        if (space.Visited)
            return false;
        space.Visited = true;

        if (space == goal)
        {
            space.IsSolution = true;
            return true;
        }

        space.IsSolution =
            space.Right is not null && Dfs(space.Right, goal) ||
            space.Bottom is not null && Dfs(space.Bottom, goal) ||
            space.Left is not null && Dfs(space.Left, goal) ||
            space.Top is not null && Dfs(space.Top, goal);

        if (space.IsSolution) Distance++;

        return space.IsSolution;
    }

    private bool Bfs(Space start, Space goal)
    {
        var queue = new Queue<Space>();
        var comeMap = new Dictionary<Space, Space>();
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var crr = queue.Dequeue();

            if (crr.Visited)
                continue;

            crr.Visited = true;

            if (crr == goal)
                
                break;

            var neighborhood = new[]
            {
                crr.Top, crr.Bottom, crr.Left, crr.Right
            };

            foreach (var neighbor in neighborhood)
            {
                if (neighbor is null || neighbor.Visited)
                    continue;

                comeMap.TryAdd(neighbor, crr);
                queue.Enqueue(neighbor);
            }
        }

        var it = goal;
        while (it != start)
        {
            it.IsSolution = true;
            Distance++;
            it = comeMap[it];
        }

        start.IsSolution = true;
        Distance++;

        return true;
    }

    private bool Dijkstra(Space start, Space goal)
    {
        var queue = new PriorityQueue<Space, float>();
        var distMap = new Dictionary<Space, float>();
        var comeMap = new Dictionary<Space, Space>();

        distMap[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var crr = queue.Dequeue();
            crr.Visited = true;

            if (crr == goal)
                break;

            var neighborhood = new[]
            {
                crr.Top, crr.Bottom, crr.Left, crr.Right
            };

            foreach (var neighbor in neighborhood)
            {
                if (neighbor is null)
                    continue;

                distMap.TryAdd(neighbor, float.PositiveInfinity);
                comeMap.TryAdd(neighbor, null!);

                var newDist = distMap[crr] + 1;
                
                if (newDist > distMap[neighbor])
                    continue;

                distMap[neighbor] = newDist;
                comeMap[neighbor] = crr;
                queue.Enqueue(neighbor, newDist);
            }
        }

        var it = goal;
        while (it != start)
        {
            it.IsSolution = true;
            Distance++;
            it = comeMap[it];
        }

        start.IsSolution = true;
        Distance++;
        return true;
    }

    private bool AStar(Space start, Space goal)
    {
        var queue = new PriorityQueue<Space, float>();
        var distMap = new Dictionary<Space, float>();
        var comeMap = new Dictionary<Space, Space>();

        distMap[start] = 0;
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var crr = queue.Dequeue();
            crr.Visited = true;

            if (crr == goal)
                break;

            var neighborhood = new[]
            {
                crr.Top, crr.Bottom, crr.Left, crr.Right
            };

            foreach (var neighbor in neighborhood)
            {
                if (neighbor is null)
                    continue;

                distMap.TryAdd(neighbor, float.PositiveInfinity);
                comeMap.TryAdd(neighbor, null!);

                var newDist = distMap[crr] + 1;

                if (newDist > distMap[neighbor]) continue;

                var dx = neighbor.X - goal.X;
                var dy = neighbor.Y - goal.Y;
                var penalty = MathF.Sqrt(dx * dx + dy * dy);
                
                distMap[neighbor] = newDist;
                comeMap[neighbor] = crr;
                
                queue.Enqueue(neighbor, newDist + penalty);
            }
        }

        var it = goal;
        while (it != start)
        {
            it.IsSolution = true;
            Distance++;
            it = comeMap[it];
        }

        start.IsSolution = true;
        Distance++;
        return true;
    }
}
using BlazorAppAstar.Models;

namespace BlazorAppAstar.Algorithm;

public class AStarService
{
    public List<PathNode> RunAStar(PathNode[,] grid, PathNode start, PathNode end)
    {
        var openSet = new List<PathNode> { start };
        var closedSet = new HashSet<PathNode>();

        start.GCost = 0;
        start.HCost = Heuristic(start, end);

        while (openSet.Count > 0)
        {
            var current = openSet.OrderBy(n => n.FCost).First();

            if (current == end)
                return ReconstructPath(end);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (var neighbor in GetNeighbors(grid, current))
            {
                if (neighbor.IsWall || closedSet.Contains(neighbor))
                    continue;

                int tentativeG = current.GCost + 1;

                if (tentativeG < neighbor.GCost || !openSet.Contains(neighbor))
                {
                    neighbor.GCost = tentativeG;
                    neighbor.HCost = Heuristic(neighbor, end);
                    neighbor.Previous = current;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return new List<PathNode>();
    }

    private List<PathNode> ReconstructPath(PathNode end)
    {
        var path = new List<PathNode>();
        var current = end;

        while (current.Previous != null)
        {
            path.Add(current);
            current = current.Previous;
        }

        path.Reverse();
        return path;
    }

    private int Heuristic(PathNode a, PathNode b)
    {
        return Math.Abs(a.Row - b.Row) + Math.Abs(a.Col - b.Col);
    }

    private IEnumerable<PathNode> GetNeighbors(PathNode[,] grid, PathNode node)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        int r = node.Row;
        int c = node.Col;

        if (r > 0)      yield return grid[r - 1, c];
        if (r < rows-1) yield return grid[r + 1, c];
        if (c > 0)      yield return grid[r, c - 1];
        if (c < cols-1) yield return grid[r, c + 1];
    }
}
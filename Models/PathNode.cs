namespace BlazorAppAstar.Models;

public class PathNode
{
    public int Row { get; set; }
    public int Col { get; set; }
    public bool IsWall { get; set; }
    public bool IsStart { get; set; }
    public bool IsEnd { get; set; }

    // A* fields
    public int GCost { get; set; } = int.MaxValue;
    public int HCost { get; set; }
    public int FCost => GCost + HCost;

    public PathNode? Previous { get; set; }

    // Visual
    public bool IsPath { get; set; }
}
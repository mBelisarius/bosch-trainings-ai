namespace Artin.Collections;

public interface INode : IEquatable<INode>
{
    object? Data { get; set; }

    IReadOnlyCollection<IEdge> Neighbors { get; }

    int CountNeighbours { get; }
}
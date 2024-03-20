namespace Artin.Collections;

public interface IEdge
{
    object? Data       { get; set; }
    INode   SourceNode { get; set; }
    INode   TargetNode { get; set; }
}
namespace Artin.Collections.Tree;

public class TreeEdge<T> : IEdge
{
    private T?          _data;
    private TreeNode<T> _sourceNode = null!;
    private TreeNode<T> _targetNode = null!;

    public object? Data
    {
        get => _data;
        set => _data = (T?)value;
    }

    public INode SourceNode
    {
        get => _sourceNode;
        set => _sourceNode = (TreeNode<T>)value;
    }

    public INode TargetNode
    {
        get => _targetNode;
        set => _targetNode = (TreeNode<T>)value;
    }

    public TreeEdge(
        INode source,
        INode target
    )
    {
        SourceNode = source;
        TargetNode = target;
    }
}
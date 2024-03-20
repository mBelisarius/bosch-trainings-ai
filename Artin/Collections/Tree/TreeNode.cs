using System.Text;

namespace Artin.Collections.Tree;

public class TreeNode<T> : INode
{
    private T?                _data;
    private List<TreeEdge<T>> _neighbors;

    public object? Data
    {
        get => _data;
        set => _data = (T?)value;
    }

    public IReadOnlyCollection<IEdge> Neighbors => _neighbors;
    
    public IReadOnlyCollection<IEdge> Children => _neighbors;

    public int CountNeighbours => _neighbors.Count;
    
    public int CountChildren => _neighbors.Count;

    public bool Equals(INode? other)
    {
        if (other is not TreeNode<T> node)
            return false;
        
        return EqualityComparer<T>.Equals(node.Data, Data);
    }

    public override bool Equals(object? obj) => Equals(obj as TreeNode<T>);
    public override int GetHashCode() => (_data, _neighbors).GetHashCode();
    
    public static bool operator ==(TreeNode<T> a, TreeNode<T> b)
        => a.Equals(b);

    public static bool operator !=(TreeNode<T> a, TreeNode<T> b)
        => !(a == b);

    public TreeNode(
        T data,
        List<TreeEdge<T>>? children = null
    )
    {
        Data = data;
        _neighbors = children ?? new List<TreeEdge<T>>();
    }

    public TreeNode<T> AddChild(TreeNode<T> node)
    {
        _neighbors.Add(new TreeEdge<T>(this, node));

        return this;
    }

    public TreeNode<T> RemoveChild(TreeNode<T> node)
    {
        _neighbors.RemoveAll(edge => ((TreeNode<T>)edge.TargetNode).Equals(node));

        return this;
    }

    public void ClearBranch()
    {
        _neighbors.Clear();
    }

    public override string ToString()
    {
        return ToString("", true, true);
    }

    private string ToString(string indent, bool isLast, bool isRoot)
    {
        var result = new StringBuilder(indent);

        if (!isLast)
        {
            // result.Append(Parent?.Children.LastOrDefault() == this
            //                   ? "\u2514\u2500\u2500\u2500"
            //                   : "\u251c\u2500\u2500\u2500");
            result.Append("\u251c\u2500\u2500\u2500");
            indent += "\u2502   ";
        }
        else if (!isRoot)
        {
            result.Append("\u2514\u2500\u2500\u2500");
            indent += "    ";
        }

        result.AppendLine(Data?.ToString());

        for (int i = 0; i < Children.Count; i++)
            result.Append(((TreeNode<T>)_neighbors[i].TargetNode).ToString(indent, i == Children.Count - 1, false));

        return result.ToString();
    }
}
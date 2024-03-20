namespace Artin.Collections.Tree;

public class Tree<T>
{
    public TreeNode<T>                Root     { get; private set; }
    public IReadOnlyCollection<IEdge> Branches => Root.Neighbors;

    public Tree(TreeNode<T> root)
    {
        Root = root;
    }

    public Tree<T> AddBranch(TreeNode<T> branch)
    {
        Root.AddChild(branch);

        return this;
    }

    public Tree<T> AddBranch(Tree<T> branch)
    {
        Root.AddChild(branch.Root);

        return this;
    }

    public override string ToString()
    {
        return Root.ToString();
    }
}
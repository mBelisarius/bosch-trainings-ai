using Artin.Collections.Tree;
using Artin.Search;

public static class ExeTree
{
    public static void Execute()
    {
         var tree = BuildTree();
         Console.WriteLine(tree);
         
         // Find the goal node using DFS
         var start = tree.Root;
         var goal = new TreeNode<int>(9);
         
         var pathDfs = Search.Dfs(start, goal);
         Console.WriteLine(pathDfs is not null ? "Goal found!" : "Goal not found.");
    }

    private static Tree<int> BuildTree()
    {
        // Tree 1 (value: 50)
        var node = new TreeNode<int>(21).AddChild(new TreeNode<int>(6));
        node = new TreeNode<int>(12)
               .AddChild(new TreeNode<int>(45))
               .AddChild(node);
        var tree1 = new Tree<int>(new TreeNode<int>(50));
        tree1.AddBranch(node);

        // Tree 2 (value: 1)
        var tree2 = new Tree<int>(new TreeNode<int>(1));
        tree2.AddBranch(new TreeNode<int>(70))
             .AddBranch(new TreeNode<int>(61));

        // Tree 3 (value: 30)
        var tree3 = new Tree<int>(new TreeNode<int>(30));
        tree3.AddBranch(new TreeNode<int>(96))
             .AddBranch(new TreeNode<int>(9));


        // Tree 4 (value: 150)
        var tree4 = new Tree<int>(new TreeNode<int>(150));
        tree4.AddBranch(tree3)
             .AddBranch(new TreeNode<int>(5))
             .AddBranch(new TreeNode<int>(11));

        // Tree 5 (value: 100)
        var tree5 = new Tree<int>(new TreeNode<int>(100));
        tree5.AddBranch(tree1)
             .AddBranch(tree2)
             .AddBranch(tree4);

        return tree5;
    }
}
using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class BinaryTreePreorderTraversal
{
    public IList<int> PreorderTraversal(TreeNode root)
    {
        if (root is null)
        {
            return new List<int>();
        }
        var ints = new List<int>(100);
        var treeNodes = new Stack<TreeNode>();
        treeNodes.Push(root);
        while (treeNodes.TryPop(out var curr))
        {
            ints.Add(curr.val);
            if (curr.right is not null)
            {
                treeNodes.Push(curr.right);
            }
            
            if (curr.left is not null)
            {
                treeNodes.Push(curr.left);
            }
        }

        return ints;
    }

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
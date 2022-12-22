using System.Collections.Generic;
using System.Linq;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/invert-binary-tree/
/// </summary>
public class BinaryTreeInvert
{
    public TreeNode InvertTree(TreeNode root)
    {
        if (root is null) return root;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.TryDequeue(out var node))
        {
            (node.left, node.right) = (node.right, node.left);

            if (node.left is not null) queue.Enqueue(node.left);
            if (node.right is not null) queue.Enqueue(node.right);
        }

        return root;
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
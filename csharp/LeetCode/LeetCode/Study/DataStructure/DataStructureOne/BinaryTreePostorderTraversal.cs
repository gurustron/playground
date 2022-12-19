using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class BinaryTreePostOrderTraversal
{
    public IList<int> PostorderTraversal(TreeNode root)
    {
        if (root is null)
        {
            return new List<int>();
        }
        var ints = new List<int>(100);
        var traverse = new Stack<TreeNode>();
        var visited = new HashSet<TreeNode>();
        traverse.Push(root);
        while (traverse.TryPop(out var curr))
        {
            if (!visited.Contains(curr))
            {   
                if (curr.right is not null)
                {
                    traverse.Push(curr.right);
                }

                if (curr.left is not null)
                {
                    traverse.Push(curr.left);
                }
                
                traverse.Push(curr);
                visited.Add(curr);
            }
            else
            {
                ints.Add(curr.val);
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
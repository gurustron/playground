using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

public class BSTTwoSum
{
    public bool FindTarget(TreeNode root, int k)
    {
        var values = new HashSet<int>();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        while (queue.TryDequeue(out var node))
        {
            if (values.Contains(k - node.val)) return true;
            
            if(node.left is not null) queue.Enqueue(node.left);
            if(node.right is not null) queue.Enqueue(node.right);
            values.Add(node.val);
        }

        return false;
    }
}
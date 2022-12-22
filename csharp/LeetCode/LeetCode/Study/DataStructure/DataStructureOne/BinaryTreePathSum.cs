namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/path-sum/
/// </summary>
public class BinaryTreePathSum
{
    public bool HasPathSum(TreeNode root, int targetSum)
    {
        if (root is null) return false;

        if (root.left is null && root.right is null) return root.val == targetSum;

        targetSum -= root.val;
        return HasPathSum(root.left, targetSum) || HasPathSum(root.right, targetSum);
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
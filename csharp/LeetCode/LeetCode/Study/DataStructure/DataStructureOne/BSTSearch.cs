namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/search-in-a-binary-search-tree
/// </summary>
public class BSTSearch
{
    public TreeNode SearchBST(TreeNode root, int val)
    {
        if (root is null || root.val == val) return root;

        if (val < root.val)
        {
            return SearchBST(root.left, val);
        }

        return SearchBST(root.right, val);
    }

    public class TreeNode
    {
        public TreeNode left;
        public TreeNode right;
        public int val;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
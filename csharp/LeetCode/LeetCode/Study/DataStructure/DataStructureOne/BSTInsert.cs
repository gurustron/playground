namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/insert-into-a-binary-search-tree
/// </summary>
public class BSTInsert
{
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root is null) return new TreeNode(val);

        var parent = root;
        var subTree = root;
        bool isLeft = false;
        while (subTree is not null)
        {
            parent = subTree;

            if (subTree.val > val)
            {
                subTree = subTree.left;
                isLeft = true;
            }
            else
            {
                subTree = subTree.right;
                isLeft = false;
            }
        }

        if (isLeft)
        {
            parent.left = new TreeNode(val);
        }
        else
        {
            parent.right = new TreeNode(val);
        }

        return root;
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
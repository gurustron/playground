namespace LeetCode.Study.DataStructure.DataStructureOne;

public class BSTValidate
{
    public bool IsValidBST(TreeNode root)
    {
        return Inner(root.left, root.val, null)
               && Inner(root.right, null, root.val);
        
        bool Inner(TreeNode r, int? lsThen, int? gtThen)
        {
            if (r is null) return true;

            if (lsThen is { } ls && ls <= r.val) return false;
            if (gtThen is { } gt && gt >= r.val) return false;

            return Inner(r.left, r.val, gtThen) 
                   && Inner(r.right, lsThen, r.val);
        }
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